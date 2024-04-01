using EventApi.http.Requests;
using EventApi.http.Responses;
using EventApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventApi.Services.Interfaces;
using TasksApi.Controllers;
using EventApi.Models;
using System.Security.Claims;
using System.Web.Http.Cors;
using System.Net.Http.Headers;
using System.Net;

namespace EventApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : BaseApiController
    {
        
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;


        public UsersController(IUserService userService, ITokenService tokenService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new TokenResponse
                {
                    ErrorMessage = "Missing login details",
                    ErrorCode = "Login"
                });
            }

            var loginResponse = await _userService.LoginAsync(loginRequest);

            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.ErrorMessage
                });
            }
            Response.Cookies.Append("X-Refresh-Token", loginResponse.RefreshToken,
                 new CookieOptions()
                 {
                     Secure = true,
                     HttpOnly = true,
                     SameSite = SameSiteMode.None,
                     Expires = DateTimeOffset.Now.AddDays(14)
                 });
            return Ok(loginResponse);
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var logout = await _userService.LogoutAsync(UserID);

            if (!logout.Success)
            {
                return UnprocessableEntity(logout);
            }

            return Ok();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            
            var signupResponse = await _userService.SignupAsync(signupRequest);

            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }

            return Ok(signupResponse.Email);
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> Info()
        {
            var userResponse = await _userService.GetInfoAsync(UserID);

            if (!userResponse.Success)
            {
                return UnprocessableEntity(userResponse);
            }

            return Ok(userResponse);

        }
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            string cookie = "";
            Request.Cookies.TryGetValue("X-Refresh-Token",out cookie);
            if (refreshTokenRequest == null || string.IsNullOrEmpty(cookie))
            {
                return BadRequest(new TokenResponse
                {
                    ErrorMessage = "Missing refresh token details",
                    ErrorCode = "RefreshValidation"
                });
            }
          
            var validateRefreshTokenResponse = 
                await _tokenService.ValidateRefreshTokenAsync(new Tuple<string, int>(cookie,refreshTokenRequest.UserId));

            if (!validateRefreshTokenResponse.Success)
            {
                return BadRequest(validateRefreshTokenResponse);
            }

            var tokenResponse = await _tokenService.GenerateTokensAsync(validateRefreshTokenResponse.UserId);

            if (tokenResponse == null)
            {
                return BadRequest(new TokenResponse
                {
                    ErrorMessage = "Missing user",
                    ErrorCode = "RefreshValidation"
                });
            }
            
            Response.Cookies.Append("X-Refresh-Token", tokenResponse.Item2,
                 new CookieOptions()
                 {
                     Secure = true,
                     HttpOnly = true,
                     SameSite = SameSiteMode.None,
                     Expires = DateTimeOffset.Now.AddDays(14)
                 });
            
            return Ok(new TokenResponse { 
                AccessToken = tokenResponse.Item1,
                RefreshToken = tokenResponse.Item2,
                UserId = validateRefreshTokenResponse.UserId 
            });
        }
    }
}

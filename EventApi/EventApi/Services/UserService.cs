using EventApi.http.Responses;
using EventApi.http.Requests;
using EventApi.Models;
using Microsoft.EntityFrameworkCore;
using EventApi.Utilis;
using EventApi.Services.Interfaces;

namespace EventApi.Services
{
    
    public class UserService : IUserService
    {
        private readonly CalendarEventDbContext _eventDbContext;
        private readonly ITokenService _tokenService;

        public UserService(CalendarEventDbContext eventDbContext, ITokenService tokenService)
        {
            this._eventDbContext = eventDbContext;
            this._tokenService = tokenService;
        }
        public async Task<UserResponse> GetInfoAsync(int userId)
        {
            var user = await _eventDbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return new UserResponse
                {
                    Success = false,
                    ErrorMessage = "No user found",
                    ErrorCode = "Info"
                };
            }

            return new UserResponse
            {
                Success = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = _eventDbContext.Users.SingleOrDefault(user=>user.Email == loginRequest.Email);

            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    ErrorMessage = "Email not found",
                    ErrorCode = "Login"
                };
            }
            var passwordHash = PasswordHasher.HashUsingPbkdf2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));

            if (user.Password != passwordHash)
            {
                return new TokenResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid Password",
                    ErrorCode = "Login"
                };
            }

            var token = await Task.Run(() => _tokenService.GenerateTokensAsync(user.Id));

            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2,
                UserId = user.Id
            };
        }

        public async Task<LogoutResponse> LogoutAsync(int userId)
        {
            var refreshToken = await _eventDbContext.Tokens.FirstOrDefaultAsync(o => o.UserId == userId);

            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }

            _eventDbContext.Tokens.Remove(refreshToken);

            var saveResponse = await _eventDbContext.SaveChangesAsync();

            return new LogoutResponse { Success = true };
        }

        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            var existingUser = await _eventDbContext.Users.SingleOrDefaultAsync(user => user.Email == signupRequest.Email);
            if (existingUser != null)
            {
                return new SignupResponse
                {
                    Success = false,
                    ErrorMessage = "User already exists with this email",
                    ErrorCode = "Signup"
                };
            }
            if (signupRequest.Password != signupRequest.ConfirmPassword)
            {
                return new SignupResponse
                {
                    Success = false,
                    ErrorMessage = "Password and confirm password do not match",
                    ErrorCode = "Signup"
                };
            }
            var salt = PasswordHasher.GetSecureSalt();
            var passwordHash = PasswordHasher.HashUsingPbkdf2(signupRequest.Password, salt);

            var user = new User
            {
                Email = signupRequest.Email,
                Password = passwordHash,
                PasswordSalt = Convert.ToBase64String(salt),
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
                Timestamp = signupRequest.Ts,
            };
             await _eventDbContext.Users.AddAsync(user);

            var saveResponse = await _eventDbContext.SaveChangesAsync();

            return new SignupResponse { Success = true, Email = user.Email };
          
        }
    }
}

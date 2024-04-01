using EventApi.http.Requests;
using EventApi.http.Responses;
using EventApi.Models;
using EventApi.Services.Interfaces;
using EventApi.Utilis;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly CalendarEventDbContext _eventDbContext;

        public TokenService(CalendarEventDbContext tasksDbContext)
        {
            this._eventDbContext = tasksDbContext;
        }
        public async Task<Tuple<string, string>> GenerateTokensAsync(int userId)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(userId);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

            var userRecord = await _eventDbContext.Users.Include(o => o.Tokens).FirstOrDefaultAsync(e => e.Id == userId);

            if (userRecord == null)
            {
                return null;
            }

            var salt = PasswordHasher.GetSecureSalt();

            var refreshTokenHashed = PasswordHasher.HashUsingPbkdf2(refreshToken, salt);

            if (userRecord.Tokens.Count != 0)
            {
                await RemoveRefreshTokenAsync(userRecord);

            }
            userRecord.Tokens?.Add(new Token
            {
                ExpiryDate = DateTime.Now.AddDays(14),
                Timestamp = DateTime.Now,
                UserId = userId,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt)

            });

            await _eventDbContext.SaveChangesAsync();

            var token = new Tuple<string, string>(accessToken, refreshToken);

            return token;
        }

        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userDb = await _eventDbContext.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
                    
            if (userDb == null)
            {       
                return false;
            }       
                    
            if (userDb.Tokens.Any())
            {
                var currentRefreshToken = userDb.Tokens.First();

                _eventDbContext.Tokens.Remove(currentRefreshToken);
            }

            return false;
        }


        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(Tuple<string, int> refreshTokenRequest)
        {
            var refreshToken = await _eventDbContext.Tokens.FirstOrDefaultAsync(o => o.UserId == refreshTokenRequest.Item2);

            if (refreshToken == null)
            {
                return new ValidateRefreshTokenResponse()
                {
                    Success = false,
                    ErrorMessage = "User is not logged",
                    ErrorCode = "RefreshValidation"
                };
            }
           
            var refreshTokenToValidateHash = PasswordHasher.HashUsingPbkdf2(refreshTokenRequest.Item1, Convert.FromBase64String(refreshToken.TokenSalt));

            if (refreshToken.TokenHash != refreshTokenToValidateHash)
            {
                return new ValidateRefreshTokenResponse()
                {
                    Success = false,
                    ErrorMessage = "Invalid refresh token",
                    ErrorCode = "RefreshValidation"
                };
            }

            if (refreshToken.ExpiryDate < DateTime.Now)
            {
                return new ValidateRefreshTokenResponse()
                {
                    Success = false,
                    ErrorMessage = "Refresh token has expired",
                    ErrorCode = "RefreshValidation"
                };
            }

            return new ValidateRefreshTokenResponse()
            {
                Success = true,
                UserId = refreshToken.UserId
            };
        }
    }
}

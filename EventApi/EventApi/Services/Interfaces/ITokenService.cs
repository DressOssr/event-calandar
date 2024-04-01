
using EventApi.http.Responses;
using EventApi.http.Requests;
using EventApi.Models;

namespace EventApi.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Tuple<string, string>> GenerateTokensAsync(int userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(Tuple<string,int> refreshToken);
        Task<bool> RemoveRefreshTokenAsync(User user);
    }
}

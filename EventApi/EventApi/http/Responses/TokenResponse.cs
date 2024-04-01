using System.Text.Json.Serialization;

namespace EventApi.http.Responses
{
    public class TokenResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
      
    }
}

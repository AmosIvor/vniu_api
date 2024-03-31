using vniu_api.ViewModels;

namespace vniu_api.Models.Responses
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }

        public string? ExpiresAccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? ExpiresRefreshToken { get; set; }

        public UserVM? User { get; set; }
    }

  
}

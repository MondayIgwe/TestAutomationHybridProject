using System.Text.Json.Serialization;

namespace API.Services.Automation.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;
    }
}

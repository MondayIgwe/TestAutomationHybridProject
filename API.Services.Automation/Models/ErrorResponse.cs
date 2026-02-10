using System.Text.Json.Serialization;

namespace API.Services.Automation.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; } = string.Empty;
    }
}

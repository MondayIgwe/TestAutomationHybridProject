using System.Text.Json.Serialization;

namespace API.Services.Automation.Models
{
    public class OrderResponse
    {
        [JsonPropertyName("created")]
        public bool Created { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
    }
}

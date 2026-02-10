using System.Text.Json.Serialization;

namespace API.Services.Automation.Models
{
    public class OrderRequest
    {
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;
    }
}

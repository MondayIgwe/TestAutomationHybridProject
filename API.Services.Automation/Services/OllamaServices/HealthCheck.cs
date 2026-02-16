using API.Services.Automation.Core;
using RestSharp;
using System.Net;

namespace API.Services.Automation.Services.OllamaServices
{
    public class HealthCheck
    {
        private readonly RestClient _client;

        public HealthCheck(string ollamaBaseApiUrl)
        {
            _client = ApiClient.GetRestClientAsync(ollamaBaseApiUrl)!;
        }

        public async Task<RestResponse> CheckOllamaServiceHealthAsync()
        {
            var request = new RestRequest("/", Method.Get);
            var response = await _client.ExecuteAsync(request);
            
            LogHealthStatus(response);
            return response;
        }

        public async Task<bool> IsServiceHealthyAsync()
        {
            try
            {
                var response = await CheckOllamaServiceHealthAsync();
                return response.IsSuccessful && response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ollama service health check failed: {ex.Message}");
                return false;
            }
        }

        private void LogHealthStatus(RestResponse response)
        {
            if (response.IsSuccessful)
            {
                Console.WriteLine("✓ Ollama service is healthy.");
                Console.WriteLine($"  Status Code: {response.StatusCode}");
                Console.WriteLine($"  Response: {response.Content}");
            }
            else
            {
                Console.WriteLine($"✗ Ollama service health check failed.");
                Console.WriteLine($"  Status Code: {response.StatusCode}");
                Console.WriteLine($"  Error Message: {response.ErrorMessage}");
                Console.WriteLine($"  Error Exception: {response.ErrorException?.Message}");
            }
        }
    }
}

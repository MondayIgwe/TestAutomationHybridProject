using API.Services.Automation.Models;
using Bogus;
using RestSharp;
using System.Text.Json;
using static Libraries.Automation.Utils.ReusableValues;

namespace API.Services.Automation.Core
{
    public static class ApiClient
    {
        public static RestClient? GetRestClientAsync()
        {
            var options = new RestClientOptions(BaseApiUrl);
            return new RestClient(options);
        }

        public static RestRequest? GetRestRequestAsync(string endpoint, Method method)
        {
            var faker = new Faker();
            var ClientName = faker.Name.FullName();
            var ClientEmail = faker.Internet.Email();

            var request = new RestRequest(endpoint, method);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
                " + "\n" +
                            @$"   ""clientName"": ""{ClientName}"",
                " + "\n" +
                            @$"   ""clientEmail"": {ClientEmail}""
                " + "\n" +
            @"}";

            return request.AddStringBody(body, DataFormat.Json);
        }

        public static async Task<RestResponse?> GetRestResponseAsync(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync(request);
        }

        public static async Task<string?> GetTokenAsync()
        {
            var client = GetRestClientAsync();
            var request = GetRestRequestAsync("/api-clients/", Method.Post);
            var response = await GetRestResponseAsync(client!, request!);

            if (response?.Content != null && response.IsSuccessful)
            {
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(response.Content);
                return tokenResponse?.AccessToken;
            }

            return null;
        }

        public static T? DeserializeResponse<T>(string? jsonContent)
        {
            if (string.IsNullOrWhiteSpace(jsonContent))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(jsonContent);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization failed: {ex.Message}");
                return default;
            }
        }
    }
}

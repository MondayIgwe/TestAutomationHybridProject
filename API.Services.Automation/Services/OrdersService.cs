using API.Services.Automation.Core;
using API.Services.Automation.Models;
using RestSharp;
using System.Text.Json;

namespace API.Services.Automation.Services
{
    public class OrdersService
    {
        private readonly RestClient _client;
        private readonly string _authToken;

        public OrdersService(string authToken)
        {
            _client = ApiClient.GetRestClientAsync()!;
            _authToken = authToken;
        }

        public async Task<bool> ValidateEndpointAsync(string endpoint, Method method = Method.Get)
        {
            try
            {
                var request = new RestRequest(endpoint, method);
                var response = await _client.ExecuteAsync(request);
                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Endpoint validation failed: {ex.Message}");
                return false;
            }
        }

        // Encapsulate API endpoint logic
        public async Task<RestResponse> CreateOrderAsync(string ordersEndpoint, OrderRequest orderRequest)
        {
            var request = new RestRequest(ordersEndpoint, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_authToken}");

            var jsonBody = JsonSerializer.Serialize(orderRequest);
            request.AddStringBody(jsonBody, DataFormat.Json);

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetOrderAsync(string ordersEndpoint, string orderId)
        {
            var request = new RestRequest($"{ordersEndpoint}/{orderId}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {_authToken}");

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateOrderAsync(string ordersEndpoint, string orderId, OrderRequest orderRequest)
        {
            var request = new RestRequest($"{ordersEndpoint}/{orderId}", Method.Patch);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_authToken}");

            var jsonBody = JsonSerializer.Serialize(orderRequest);
            request.AddStringBody(jsonBody, DataFormat.Json);

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteOrderAsync(string ordersEndpoint, string orderId)
        {
            var request = new RestRequest($"{ordersEndpoint}/{orderId}", Method.Delete);
            request.AddHeader("Authorization", $"Bearer {_authToken}");

            return await _client.ExecuteAsync(request);
        }

        // Helper methods for response validation
        public OrderResponse? GetOrderResponseFromJson(string json)
        {
            return ApiClient.DeserializeResponse<OrderResponse>(json);
        }

        public ErrorResponse? GetErrorResponseFromJson(string json)
        {
            return ApiClient.DeserializeResponse<ErrorResponse>(json);
        }
    }
}

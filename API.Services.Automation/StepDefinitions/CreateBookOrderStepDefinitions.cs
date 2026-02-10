using API.Services.Automation.Core;
using RestSharp;
using static Libraries.Automation.Utils.ReusableValues;
using Shouldly;
using API.Services.Automation.Hooks;

namespace API.Services.Automation.StepDefinitions
{
    [Binding]
    public class CreateBookOrderStepDefinitions : BaseTest
    {
        private RestResponse? response;

        [Given("I have the following book order details:")]
        public void GivenIHaveTheFollowingBookOrderDetails(DataTable dataTable)
        {

            foreach (var row in dataTable.Rows)
            {
                var field = row["Field"];
                var value = row["Value"];

                switch (field)
                {
                    case "bookId":
                        _orderRequest.BookId = GenerateRandomNumber(0, 20)  + int.Parse(value);
                        break;
                    case "customerName":
                        _orderRequest.CustomerName = GenerateRandomNumericString(2) + value;
                        break;
                }
            }

            _orderRequest.ShouldNotBeNull("Order request should be prepared before submission");
        }

        [When("I submit a POST request to create the {string}")]
        public async Task WhenISubmitAPOSTRequestToCreateThe(string ordersEndpoint)
        {
            response = await _ordersService.CreateOrderAsync(ordersEndpoint, _orderRequest);
            Console.WriteLine($"Response Status: {response?.StatusCode}");

        }
   

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            var actualStatusCode = (int)(response?.StatusCode ?? 0);
            actualStatusCode.ShouldBe(expectedStatusCode, $"Expected status code {expectedStatusCode}, but got {actualStatusCode}");
            Console.WriteLine($"✓ Status code verified: {actualStatusCode}");
        }

        [Then("the response should contain an {string}")]
        public void ThenTheResponseShouldContainAn(string fieldName)
        {
            response?.Content.ShouldNotBeNullOrEmpty("Response content should not be null or empty");

            var orderResponse = _ordersService.GetOrderResponseFromJson(response!.Content!);
            orderResponse.ShouldNotBeNull("Failed to deserialize order response");

            if (fieldName.ToLower() == "orderid")
            {
                orderResponse.OrderId.ShouldNotBeNullOrEmpty($"Response should contain '{fieldName}'");
            }
        }

        [Then("the order should be created successfully")]
        public void ThenTheOrderShouldBeCreatedSuccessfully()
        {
            response?.Content.ShouldNotBeNullOrEmpty("Response content should not be null");

            var orderResponse = _ordersService.GetOrderResponseFromJson(response!.Content!);
            orderResponse.ShouldNotBeNull("Failed to deserialize order response");
            orderResponse.Created.ShouldBeTrue("Order should be created successfully");
            orderResponse.OrderId.ShouldNotBeNullOrEmpty("Order should be created successfully");
        }
    }
}

using API.Services.Automation.Hooks;
using API.Services.Automation.Models;
using API.Services.Automation.Services;
using API.Services.Automation.Services.OllamaServices;
using static Libraries.Automation.Utils.ReusableValues;

namespace API.Services.Automation.Core
{
    public class BaseTest
    {
        public OrdersService _ordersService;
        public OrderRequest _orderRequest;
        public HealthCheck _healthCheck;
        
        public BaseTest()
        {
            _orderRequest = new OrderRequest();
            _healthCheck = new HealthCheck(OllamaBaseUrl);
            _ordersService = new OrdersService(TestHooks.ApiClientToken!);
        }
    }
}

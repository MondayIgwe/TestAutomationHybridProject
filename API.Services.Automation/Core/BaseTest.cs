using API.Services.Automation.Hooks;
using API.Services.Automation.Models;
using API.Services.Automation.Services;
namespace API.Services.Automation.Core
{
    public class BaseTest
    {
        public OrdersService _ordersService;
        public OrderRequest _orderRequest;
        public BaseTest()
        {
            _orderRequest = new OrderRequest();

            _ordersService = new OrdersService(TestHooks.ApiClientToken!);
        }
    }
}

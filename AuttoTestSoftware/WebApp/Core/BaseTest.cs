using AuttoTestSoftware.WebApp.PageObjects.JustFlight;

namespace AuttoTestSoftware.WebApp.Core
{
    public class BaseTest
    {
        public readonly AccountLoginPage accountLoginPage;
        public BaseTest()
        {
            accountLoginPage ??= new AccountLoginPage();
        }
    }
}

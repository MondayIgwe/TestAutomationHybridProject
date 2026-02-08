using AuttoTestSoftware.PageObjects.JustFlight;

namespace AuttoTestSoftware.Core
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

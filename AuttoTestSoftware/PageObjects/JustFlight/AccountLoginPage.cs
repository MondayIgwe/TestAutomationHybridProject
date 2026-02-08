using AuttoTestSoftware.Support;
using Shouldly;
using static AuttoTestSoftware.Support.Locators;

namespace AuttoTestSoftware.PageObjects.JustFlight
{
    public class AccountLoginPage
    {
        private readonly SeleniumActions seleniumActions;

        public AccountLoginPage()
        {
            seleniumActions = new SeleniumActions();
            seleniumActions.ShouldNotBe(null);
        }

        public void OpenAccount()
        {
            seleniumActions.Click(MyAccount);
            seleniumActions.GetText(AccountLoginText).ShouldBe("Account Login");
        }
        public Task EnterEmail(string emailAddress)
        {
            Task.Run(() =>
            {
                seleniumActions.EnterText(Email, emailAddress);
            });

            return Task.CompletedTask;
        }

        public Task EnterPassword(string password)
        {
            seleniumActions.EnterText(Password, password);
            return Task.CompletedTask;
        }

        public Task ClickLogin()
        {
            seleniumActions.Click(LoginButton);
            return Task.CompletedTask;
        }
    }
}

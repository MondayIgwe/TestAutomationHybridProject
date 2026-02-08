using AuttoTestSoftware.WebApp.Core;

namespace AuttoTestSoftware.WebApp.StepDefinitions.JustFlight
{
    [Binding]
    public class AccountLoginStepDefinitions : BaseTest
    {

        [Given("I am on the account login page")]
        public void GivenIAmOnTheAccountLoginPage()
        {
            accountLoginPage.OpenAccount();
        }

        [When("I enter a valid email {string} and password {string}")]
        public void WhenIEnterAValidEmailAndPassword(string email, string password)
        {
            accountLoginPage.EnterEmail(email).Wait();
            accountLoginPage.EnterPassword(password).Wait();
        }

        [When("I submit the login form")]
        public void WhenISubmitTheLoginForm()
        {
            accountLoginPage.ClickLogin().Wait();
        }

        [Then("I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
        }

        [Then("I should see my account dashboard")]
        public void ThenIShouldSeeMyAccountDashboard()
        {
        }

        [When("I enter email {string} and password {string}")]
        public void WhenIEnterEmailAndPassword(string p0, string wrongpass)
        {
        }

        [Then("I should see an error message {string}")]
        public void ThenIShouldSeeAnErrorMessage(string p0)
        {
        }

    }
}

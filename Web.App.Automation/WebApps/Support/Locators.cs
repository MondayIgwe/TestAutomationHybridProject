using OpenQA.Selenium;

namespace AuttoTestSoftware.WebApp.Support
{
    public class Locators
    {
        public static By MyAccount => By.Id("btnViewAccount");
        public static By Email => By.CssSelector("input#UserName");
        public static By Password => By.CssSelector("input#Password");
        public static By LoginButton => By.CssSelector("input#btnLogin");
        public static By AccountLoginText => By.XPath("//h4[text()='Account Login']");

        
    }
}

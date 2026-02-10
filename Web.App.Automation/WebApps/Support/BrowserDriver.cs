using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AuttoTestSoftware.WebApp.Support
{
    public static class BrowserDriver
    {
        public static IWebDriver? Driver { get; private set; }

        public static void Start(string runMode)
        {
            if (Driver != null) return;
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            if (Equals(runMode, "headedless"))
            {
                options.AddArgument("--headless=new");
             
                Driver = new ChromeDriver(options);
                Driver.Navigate().GoToUrl(Commons.BaseUrl);
                Driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
            }
            else
            {
                Driver = new ChromeDriver(options);
                Driver.Navigate().GoToUrl(Commons.BaseUrl);
                Driver.Manage().Window.Maximize();
            }


        }

        public static void Quit()
        {
            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            finally
            {
                Driver = null;
            }
        }
    }
}

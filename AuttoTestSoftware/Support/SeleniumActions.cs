using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AuttoTestSoftware.Support
{
    public class SeleniumActions : ISelenuimActions
    {
        private IWebDriver Driver => BrowserDriver.Driver ?? throw new InvalidOperationException("BrowserDriver not started. Call BrowserDriver.Start() before using SeleniumActions.");

        public void Click(IWebElement element)
        {
            element.Click();
        }

        public string GetText(By by, TimeSpan? timeout = null)
        {
            var el = Find(by, timeout);
            var value = el.GetAttribute("value");
            return string.IsNullOrEmpty(value) ? el.Text : value;
        }


        public object? ExecuteScript(string script, params object[] args)
        {
            if (Driver is IJavaScriptExecutor js)
            {
                return js.ExecuteScript(script, args);
            }

            throw new NotSupportedException("Driver does not support JavaScript execution");
        }

        public IWebElement Find(By by, TimeSpan? timeout = null)
        {

            if (!timeout.HasValue)
            {
                return Driver.FindElement(by);
            }

            var wait = new WebDriverWait(Driver, timeout.Value);
            return wait.Until(d =>
            {
                var els = d.FindElements(by);
                return els.Count > 0 ? els[0] : null;
            });
        }

        public bool TryFind(By by, out IWebElement? element, TimeSpan? timeout = null)
        {
            try
            {
                element = Find(by, timeout);
                return element != null;
            }
            catch
            {
                element = null;
                return false;
            }
        }

        public void Click(By by, TimeSpan? timeout = null)
        {
            var el = Find(by, timeout);
            el.Click();
        }

        public void EnterText(By by, string text, TimeSpan? timeout = null)
        {
            var el = Find(by, timeout);
            el.Clear();
            el.SendKeys(text);
        }

        public string GetText(byte by, TimeSpan? timeOut = null)
        {
            throw new NotImplementedException();
        }

        public void WaitForVisible(By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(Driver, timeout);
            wait.Until(d =>
            {
                var els = d.FindElements(by);
                return els.Count > 0 && els[0].Displayed && els[0].Enabled;
            });
        }
    }
}

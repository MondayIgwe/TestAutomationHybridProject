using OpenQA.Selenium;

namespace AuttoTestSoftware.Support
{
    public interface ISelenuimActions
    {
        IWebElement Find(By by, TimeSpan? timeout = null);
        bool TryFind(By by, out IWebElement? element, TimeSpan? timeOut = null);
        void Click(By by, TimeSpan? timeOut = null);
        void Click(IWebElement element);
        void EnterText(By by, string text, TimeSpan? timeout = null);
        string GetText(By by, TimeSpan? timeOut = null);
        void WaitForVisible(By by, TimeSpan timeout);
        object? ExecuteScript(string script, params object[] args);
    }
}

using AuttoTestSoftware.Support;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace AuttoTestSoftware.Hooks
{
    [Binding]
    public class TestHook
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            BrowserDriver.Start("headed");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                var driver = BrowserDriver.Driver;
                if (driver != null)
                {
                    // If the scenario failed, capture a screenshot
                    var status = TestContext.CurrentContext.Result.Outcome.Status;
                    if (status == TestStatus.Failed)
                    {
                        try
                        {
                            if (driver is ITakesScreenshot ts)
                            {
                                var screenshot = ts.GetScreenshot();
                                var outDir = TestContext.CurrentContext.WorkDirectory ?? AppContext.BaseDirectory;
                                var fileName = $"screenshot_{SanitizeFileName(TestContext.CurrentContext.Test.Name)}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.png";
                                var full = Path.Combine(outDir, fileName);
                                screenshot.SaveAsFile(full, ScreenshotImageFormat.Png);
                                TestContext.AddTestAttachment(full, "Screenshot on failure");
                            }
                        }
                        catch (Exception ex)
                        {
                            TestContext.WriteLine($"Failed to capture screenshot: {ex}");
                        }
                    }
                }
            }
            finally
            {
                // Ensure browser is always closed after scenario
                BrowserDriver.Quit();
            }
        }

        private static string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }
    }
}

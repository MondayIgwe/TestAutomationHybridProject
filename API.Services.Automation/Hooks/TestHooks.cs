using API.Services.Automation.Core;
using static Libraries.Automation.Utils.ReusableValues;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace API.Services.Automation.Hooks
{
    [Binding]
    public class TestHooks
    {
        public static string? ApiClientToken { get; private set; } 

        [BeforeTestRun]
        public async static Task BeforeTestRun()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("API Test Automation Suite Starting...");
            Console.WriteLine($"Test Run Started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("========================================");

            ApiClientToken = await ApiClient.GetTokenAsync();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("API Test Automation Suite Completed");
            Console.WriteLine($"Test Run Ended at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("========================================");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"\n--- Starting Feature: {featureContext.FeatureInfo.Title} ---");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"--- Completed Feature: {featureContext.FeatureInfo.Title} ---\n");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine($"\n>> Scenario: {scenarioContext.ScenarioInfo.Title}");
            Console.WriteLine($"   Tags: {string.Join(", ", scenarioContext.ScenarioInfo.Tags)}");
            Console.WriteLine($"   Started at: {DateTime.Now:HH:mm:ss}");
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;

            Console.WriteLine($"   Completed at: {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"   Status: {status}");

            if (status == TestStatus.Failed)
            {
                var error = TestContext.CurrentContext.Result.Message;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;

                Console.WriteLine("\n!! SCENARIO FAILED !!");
                Console.WriteLine($"   Error: {error}");
                Console.WriteLine($"   Stack Trace: {stackTrace}");
            }

            Console.WriteLine($"<< End Scenario: {scenarioTitle}\n");
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            Console.WriteLine($"   → Executing Step: {stepInfo.StepDefinitionType} {stepInfo.Text}");
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            var status = scenarioContext.StepContext.Status;

            if (status == ScenarioExecutionStatus.TestError)
            {
                Console.WriteLine($" ✗ Step Failed: {stepInfo.Text}");
            }
            else if (status == ScenarioExecutionStatus.StepDefinitionPending)
            {
                Console.WriteLine($" ⚠ Step Pending: {stepInfo.Text}");
            }
        }
    }
}

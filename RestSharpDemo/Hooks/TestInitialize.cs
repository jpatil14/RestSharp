using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using RestSharpDemo.Base;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestSharpDemo.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        private Settings _settings;
        public TestInitialize(Settings settings)
        {
            _settings = settings;
        }
        [BeforeTestRun]
        public static void InitializeAPIExtentReport()
        {
            var htmlReporter = new ExtentHtmlReporter(ConfigurationManager.AppSettings["extentReportPath"].ToString());
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.LoadConfig(ConfigurationManager.AppSettings["extentConfigPath"].ToString());
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Environment", "SIT1");
            extent.AddSystemInfo("Application", "Workspace Manager");
        }

        [AfterTestRun]
        public static void TearDownReprt()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);

        }

        [AfterStep]
        public void AddReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType.Equals("Given"))
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType.Equals("When"))
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType.Equals("Then"))
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType.Equals("And"))
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else
            {
                if (stepType.Equals("Given"))
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType.Equals("When"))
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType.Equals("Then"))
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType.Equals("And"))
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
            }

        }

        [BeforeScenario]
        public void TestSetup()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            _settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
        }

        [AfterScenario]
        public void afterscenario()
        {
            scenario.CreateNode<ResponseTime>("Response time in MS was " + Libraries.executionTimeInMS.ToString());
        }

    }
}

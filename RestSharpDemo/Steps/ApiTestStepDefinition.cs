using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Base;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharpDemo.Steps
{
    [Binding]
    public class ApiTestStepDefinition
    {
        private Settings _settings;
        public ApiTestStepDefinition(Settings settings)
        {
            _settings = settings;
        }

        [Given(@"I perform ""(.*)"" operation for ""(.*)""")]
        public void GivenIPerformOperationFor(string method, string url)
        {
            switch (method)
            {
                case "GET":
                    _settings.Request = new RestRequest(url, Method.GET);
                    break;
                case "PUT":
                    _settings.Request = new RestRequest(url, Method.PUT);
                    break;
                case "POST":
                    _settings.Request = new RestRequest(url, Method.POST);
                    break;
                default:
                    Assert.IsFalse(true, "Given operation in step as " + method + " is not a valid method.Please Verify");
                    break;
            }
        }

        [When(@"I post the contents as '(.*)''(.*)'")]
        public void GivenIPostTheContentsAs(string value1, string value2)
        {
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { name = value1, job = value2 });
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
        }



        //[Given(@"I perform GET operation for ""(.*)""")]
        //public void GivenIPerformGETOperationFor(string url)
        //{
        //    _settings.Request = new RestRequest(url, Method.GET);
        //}

        //[Given(@"I perform post operation ""(.*)"" with body")]
        //public void GivenIPerformPostOperationWithBody(string url, Table table)
        //{
        //    dynamic data = table.CreateDynamicInstance();
        //    _settings.Request = new RestRequest(url, Method.POST);
        //    _settings.Request.RequestFormat = DataFormat.Json;
        //    _settings.Request.AddBody(new { name = data.name.ToString(), job = data.job.ToString() });
        //    _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();

        //}

        [Then(@"I should see the status code as ""(.*)""")]
        public void ThenIShouldSeeTheStatusCodeAs(string statusCode)
        {
            Assert.That(_settings.Response.GetResponseStatusCode(), Is.EqualTo(statusCode), "Expected status code is " + statusCode + ".However Actual is " + _settings.Response.GetResponseStatusCode());
        }


        //[Given(@"I perform PUT operation for ""(.*)""")]
        //public void GivenIPerformPUTOperationFor(string url)
        //{
        //    _settings.Request = new RestRequest(url, Method.PUT);
        //}

        [When(@"I update the below contents for ""(.*)"" as ""(.*)""")]
        public void GivenIUpdateTheBelowContentsForAs(string resource, int resouceID, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request.AddUrlSegment(resource, resouceID.ToString());
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { name = data.name.ToString(), job = data.job.ToString() });
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();

        }

        //[Given(@"I perform operation for post ""(.*)""")]
        //public void GivenIPerformOperationForPost(int id)
        //{
        //    _settings.Request.AddUrlSegment("id", id.ToString());
        //    _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
        //}

        [When(@"I retrieve the contents for resource ""(.*)"" with value ""(.*)""")]
        public void GivenIRetrieveTheContentsForResourceWithValue(string resourceName, int resourceID)
        {
            _settings.Request.AddUrlSegment(resourceName, resourceID.ToString());
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
        }


        [Then(@"I should see the ""(.*)"" as ""(.*)""")]
        public void ThenIShouldSeeTheAs(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), "failing");
        }
    }
}

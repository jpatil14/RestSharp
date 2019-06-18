using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                case "DELETE":
                    _settings.Request = new RestRequest(url, Method.DELETE);
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
            _settings.Response = _settings.RestClient.ExecuteRequestWithResponseTime(_settings.Request);
        }


        [Then(@"I should see the status code as ""(.*)""")]
        public void ThenIShouldSeeTheStatusCodeAs(string statusCode)
        {
            Assert.That(_settings.Response.GetResponseStatusCode(), Is.EqualTo(statusCode), "Expected status code is " + statusCode + ".However Actual is " + _settings.Response.GetResponseStatusCode());
        }


        [When(@"I update the below contents for ""(.*)"" as ""(.*)""")]
        public void GivenIUpdateTheBelowContentsForAs(string resource, int resouceID, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request.AddUrlSegment(resource, resouceID.ToString());
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { name = data.name.ToString(), job = data.job.ToString() });
            _settings.Response = _settings.RestClient.ExecuteRequestWithResponseTime(_settings.Request);
        }


        [When(@"I retrieve the contents for resource ""(.*)"" with value ""(.*)""")]
        public void GivenIRetrieveTheContentsForResourceWithValue(string resourceName, int resourceID)
        {
            _settings.Request.AddUrlSegment(resourceName, resourceID.ToString());
            _settings.Response = _settings.RestClient.ExecuteRequestWithResponseTime(_settings.Request);
        }


        [Then(@"I should see the ""(.*)"" as ""(.*)""")]
        public void ThenIShouldSeeTheAs(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), "Expected value as " + value + " is not matching with actual value as " + _settings.Response.GetResponseObject(key));
        }

        [Then(@"I should see first_name as ""(.*)"" for id as ""(.*)""")]
        public void ThenIShouldSeeFirst_NameAsForIdAs(string firstNameValue, int idValue)
        {
            string actualResult = null;
            Boolean found = false;
            var userList = _settings.Response.DeserializeResponseInGeneric<UserList>();
            foreach (var item in userList.data)
            {
                if (item.id.Equals(idValue))
                {
                    found = true;
                    actualResult = item.first_name;
                    break;
                }
            }
            if (found)
                Assert.AreEqual(firstNameValue, actualResult);
            else
                Assert.IsTrue(false, "Expected value as " + firstNameValue + " is not present in the response");
        }

        [Then(@"I should see below values in response body of data resource")]
        public void ThenIShouldSeeBelowValuesInResponseBody(Table table)
        {
            dynamic tableData = table.CreateDynamicInstance();
            dynamic obs = JObject.Parse(_settings.Response.Content);
            Assert.AreEqual(tableData.id.ToString(), obs.data.id.ToString());
            Assert.AreEqual(tableData.email.ToString(), obs.data.email.ToString());
            Assert.AreEqual(tableData.first_name.ToString(), obs.data.first_name.ToString());
            Assert.AreEqual(tableData.last_name.ToString(), obs.data.last_name.ToString());
            Assert.AreEqual(tableData.avatar.ToString(), obs.data.avatar.ToString());
        }

    }
}

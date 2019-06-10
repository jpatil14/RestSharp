using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Base;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharpDemo.Steps
{
    [Binding]
    public class GetPostsSteps
    {
        private Settings _settings;
        public GetPostsSteps(Settings settings)
        {
            _settings = settings;
        }

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.GET);
        }

        [Given(@"I perform post operation ""(.*)"" with body")]
        public void GivenIPerformPostOperationWithBody(string url, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request = new RestRequest(url, Method.POST);
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { name = data.name.ToString(), job = data.job.ToString() });
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();

        }

        [Then(@"I should see the status code as ""(.*)""")]
        public void ThenIShouldSeeTheStatusCodeAs(string statusCode)
        {
            Assert.That(_settings.Response.GetResponseStatusCode(), Is.EqualTo(statusCode), "Expected status code is " + statusCode + ".However Actual is " + _settings.Response.GetResponseStatusCode());
        }


        [Given(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int id)
        {
            _settings.Request.AddUrlSegment("id", id.ToString());
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
        }

        [Then(@"I should see the ""(.*)"" as ""(.*)""")]
        public void ThenIShouldSeeTheAs(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), "failing");
        }
    }
}

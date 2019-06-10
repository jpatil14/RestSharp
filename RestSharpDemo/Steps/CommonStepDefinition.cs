using RestSharp;
using RestSharp.Authenticators;
using RestSharpDemo.Base;
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
    public class CommonStepDefinition
    {
        private Settings _settings;
        public CommonStepDefinition(Settings settings)
        {
            _settings = settings;
        }

        [Given(@"I authenticate the user with following details")]
        public void GivenIAuthenticateTheUserWithFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request = new RestRequest("", Method.POST);
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { name = data.userName.ToString(), job = data.password.ToString() });
            _settings.Response = _settings.RestClient.ExecutePostTaskAsync(_settings.Request).GetAwaiter().GetResult();
            var access_token = _settings.Response.GetResponseObject("access_token");
            var authenticator = new JwtAuthenticator(access_token);
            _settings.RestClient.Authenticator = authenticator;
        }


    }
}

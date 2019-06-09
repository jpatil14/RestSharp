using RestSharpDemo.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestSharpDemo.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private Settings _settings;
        public TestInitialize(Settings settings)
        {
            _settings = settings;
        }


        [BeforeScenario]
        public void TestSetup()
        {
            _settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
        }
    }
}

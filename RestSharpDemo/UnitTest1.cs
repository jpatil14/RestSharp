using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Deserializers;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PostWithTypeClassBody()
        {
            //var client = new RestClient("https://reqres.in/api/users/2");
            //var request = new RestRequest("put", Method.PUT);
            //var file = @"TestData\TestData.json";

            //request.RequestFormat = DataFormat.Json;
            //var jsonData= Newtonsoft.Json.JsonConvert.DeserializeObject<UserBody>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            //request.AddJsonBody(jsonData);
            //var restResponse = client.Execute(request);
            //var statuscode = restResponse.GetResponseStatusCode();

            var client = new RestClient("https://oms-dev-dv1.plt-l-dev-uks-ase.azure.schroders.com/api/InspectAladdinJson");
            var request = new RestRequest(Method.POST);
            //var file = @"TestData\TestData.json";

            //request.RequestFormat = DataFormat.Json;
            //var jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserBody>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            //request.AddJsonBody(jsonData);
            var restResponse = client.Execute(request);
            var statuscode = restResponse.GetResponseStatusCode();

            //var desrialize = new JsonDeserializer();
            //var output = desrialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["name"];
            //Assert.AreEqual("Jatin",response.Data.name,"Post is not created with expected resource");

        }
        [TestMethod]
        public void PostWithAsync()
        {
            var client = new RestClient("https://reqres.in/api/users");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Posts() { name = "Jatin", job = "test analyst", id = "90" });
            //var response = client.Execute<Posts>(request);
            var response = client.ExecutePostTaskAsync<Posts>(request).GetAwaiter().GetResult();
            //var desrialize = new JsonDeserializer();
            //var output = desrialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["name"];
            Assert.AreEqual("Jatin", response.Data.name, "Post is not created with expected resource");

        }

        private class UserBody
        {
            public string name { get; set; }
            public string job { get; set; }
        }
    }
}

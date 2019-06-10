using System;
using System.Collections.Generic;
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
            var client = new RestClient("https://oms-dev-dv1.plt-l-dev-uks-ase.azure.schroders.com/api/DefaultFunction1");
            var request = new RestRequest("get", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(new Posts() { name = "Jatin", job = "test analyst",id="90" });
            var response = client.Execute(request);
            var statuscode=response.StatusCode.ToString();
            
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

        
    }
}

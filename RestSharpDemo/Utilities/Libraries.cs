using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.Utilities
{
    public static class Libraries
    {
        public static Stopwatch stopwatch = new Stopwatch();
        public static double executionTimeInMS;
        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            stopwatch.Start();
            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving responsre";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }
                taskCompletionSource.SetResult(restResponse);
            });
            stopwatch.Stop();
            executionTimeInMS = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Reset();
            return await taskCompletionSource.Task;
        }

        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);
            return JSONObj;
        }

        public static string GetResponseStatusCode(this IRestResponse restResponse)
        {
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            return numericStatusCode.ToString();
        }

        public static string GetResponseObject(this IRestResponse response,string responseObject)
        {

            //var jtoken = JToken.Parse(response.Content);
            //jtoken.SelectToken(responseObject, false).ToString();
            dynamic obs = JObject.Parse(response.Content);
            foreach (var item in obs.data)
            {
                if (item.id == '4')
                {
                    Console.WriteLine(item.email);
                }
                //item.Value.ToString();
            }
            obs.SelectToken("data").Children().Count();
            //foreach (IEnumerator<KeyValuePair<string,string>> property in obs.GetEnumerator())//SelectToken("data").Children()
            //{
            //    Console.WriteLine(property.Value);
            //    foreach (JObject property1 in property.Descendants())
            //    {
            //        Console.WriteLine(property1.SelectToken(responseObject).ToString());
            //    }
            //}
            return obs.SelectToken("data").SelectToken(responseObject).ToString();
        }

    }
}

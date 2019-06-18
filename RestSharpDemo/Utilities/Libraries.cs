using Newtonsoft.Json;
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

        public static IRestResponse ExecuteRequestWithResponseTime(this RestClient client, IRestRequest request)
        {
            stopwatch.Start();
            IRestResponse response = client.Execute(request);
            stopwatch.Stop();
            executionTimeInMS = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Reset();
            return response;
        }

        public static IRestResponse ExecuteRequestWithResponseTime<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            stopwatch.Start();
            IRestResponse response = client.Execute(request);
            stopwatch.Stop();
            executionTimeInMS = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Reset();
            return response;
        }

        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);
            return JSONObj;
        }

        public static T DeserializeResponseInGeneric<T>(this IRestResponse restResponse) where T : class, new()
        {
            var deserializeObj = JsonConvert.DeserializeObject<T>(restResponse.Content);
            return (T)deserializeObj;
        }

        public static string GetResponseStatusCode(this IRestResponse restResponse)
        {
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            return numericStatusCode.ToString();
        }

        public static string GetResponseObject(this IRestResponse response, string responseObject)
        {
            var obs = JObject.Parse(response.Content);
            return obs.GetValue(responseObject).ToString();
        }
    }
}

using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.Utilities
{
    public static class Libraries
    {
        public static async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving responsre";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }
                taskCompletionSource.SetResult(restResponse);
            });
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
            
            var obs = JObject.Parse(response.Content);
            //foreach (JProperty child in obs.SelectToken("data").Children())
            //{

            //}
            return obs.SelectToken("data").SelectToken(responseObject).ToString();
        }

    }
}

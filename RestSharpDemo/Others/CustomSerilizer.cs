using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using serilizer = Newtonsoft.Json.JsonConvert;

namespace RestSharpDemo.Model
{
    public class CustomSerilizer : ISerializer
    {
        string contenttype = "application/json";
        public string ContentType { get { return contenttype; } set { contenttype = value; } }

        public string Serialize(object obj)
        {
            var data = serilizer.SerializeObject(obj);
            return data;
        }
    }
}

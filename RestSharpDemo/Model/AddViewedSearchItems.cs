using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.Model
{

    public class AddViewedSearchItems
    {
        public string team { get; set; }
        public Searchterm[] searchTerms { get; set; }
    }

    public class Searchterm
    {
        public string indexId { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

}

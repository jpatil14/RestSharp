using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.Model
{

    public class Featuredemosearchindex
    {
        public string team { get; set; }
        public searchindex searchindex { get; set; }
    }

    public class searchindex
    {
        public string type { get; set; }
        public string team { get; set; }
        public string indexid { get; set; }
        public string[] accesspermissions { get; set; }
        public string mnemonictext { get; set; }
        public string mnemonictooltip { get; set; }
        public string mnemonicbackgroundhexstring { get; set; }
        public string mnemonicforegroundhexstring { get; set; }
        public searchitem[] searchitems { get; set; }
    }

    public class searchitem
    {
        public string key { get; set; }
        public string[] searchfields { get; set; }
        public object uri { get; set; }
        public string targetid { get; set; }
        public string modulename { get; set; }
        public bool isnotifiable { get; set; }
        public namedparameter[] namedparameters { get; set; }
        public bool deleted { get; set; }
    }

    public class namedparameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}


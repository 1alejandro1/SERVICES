using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios
{
    public class Data
    {
        public string text { get; set; }
    }
    public class IAResponse
    {
        public Data data { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        

    }
}

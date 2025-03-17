using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Segurinet
{
    public class ApiSegurinetResponse
    {
        public string operation { get; set; }
        public string matricula { get; set; }
        public int codigo { get; set; }
        public string message { get; set; }
        public string nombres { get; set; }
        public List<string> lstPolicies { get; set; }
    }
}

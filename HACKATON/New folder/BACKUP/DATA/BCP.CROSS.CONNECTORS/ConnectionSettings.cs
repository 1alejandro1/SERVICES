using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.CONNECTORS
{
     public class ConnectionSettings
    {
        public InfoclientePn InfoclientePn { get; set; }
        public InfoclientePj InfoclientePj { get; set; }
    }

     public class InfoclientePn{
      public string BaseUrl {get; set; }
      public string ConsultaClienteIdc {get; set; }
      public string ConsultaClienteDbc {get; set; }
      public string Canal {get; set; }
      public string Usuario {get; set; }
      public string Password { get; set; }
    }

    public class InfoclientePj{
      public string BaseUrl {get; set; }
      public string ConsultaClienteIdc {get; set; }
      public string ConsultaClienteDbc {get; set; }
    }

}

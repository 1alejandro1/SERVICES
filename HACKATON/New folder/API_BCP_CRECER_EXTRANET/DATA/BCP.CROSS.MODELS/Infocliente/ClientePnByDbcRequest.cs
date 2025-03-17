using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePnByDbcRequest
    {
        public string Operation { get; set; }
        public ClientePn Data { get; set; }
    }
    public class ClientePn
        {
            public string Paterno { get; set; }
            public string Materno { get; set; }
            public string Nombres { get; set; }
            public string IdcNumero { get; set; }
            public string IdcTipo { get; set; }
            public string IdcExtension { get; set; }
            public string IdcComplemento { get; set; }

            public ClientePn()
            {
                IdcNumero = string.Empty;
                IdcTipo = string.Empty;
                IdcExtension = string.Empty;
                IdcComplemento = string.Empty;
            }

        }

       
    
}

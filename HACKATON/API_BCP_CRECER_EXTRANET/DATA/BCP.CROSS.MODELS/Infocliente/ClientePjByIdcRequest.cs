using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePjByIdcRequest
    {
        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string canal { get; set; }
        public string operacion { get; set; }
        public string matricula { get; set; }
    }
}

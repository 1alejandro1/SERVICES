using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS
{
    public class CobroPRResponse:DevolucionPRResponse
    {
        public string CUF { get; set; }
        public string Factura { get; set; }
    }
}
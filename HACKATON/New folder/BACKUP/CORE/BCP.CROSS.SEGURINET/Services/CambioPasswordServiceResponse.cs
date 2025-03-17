using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET.Services
{
    public class CambioPasswordServiceResponse
    {
        public bool resultadoCambioPass { get; set; }
        public long codigoSegurinet { get; set; }
        public string mensajeValidacion { get; set; }
    }
}

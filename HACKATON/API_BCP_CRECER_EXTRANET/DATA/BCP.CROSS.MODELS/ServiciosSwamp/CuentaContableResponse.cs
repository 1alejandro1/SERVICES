using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ServiciosSwamp
{
    public class CuentaContableResponse
    {
        public int IdCuentaContable { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContable { get; set; }
        public bool DebitoAbono { get; set; }
        public string ModeloServiciosCanalesBOL { get; set; }
        public string ModeloServiciosCanalesUSD { get; set; }
    }
}

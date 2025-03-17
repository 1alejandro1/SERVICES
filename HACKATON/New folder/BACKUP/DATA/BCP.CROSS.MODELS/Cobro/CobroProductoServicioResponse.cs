using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Cobro

{
    public class CobroProductoServicioResponse
    {
        public int IdCobro { get; set; }
        public string DescripcionCobro { get; set; }
        public string DescripcionFacturacion { get; set; }
        public string CodigoFacturacion { get; set; }
        public int ServiciosCanales { get; set; }
        public string TipoFacturacion { get; set; }
        public decimal Importe { get; set; }
        public string Glosa { get; set; }
        public string CuentaContable { get; set; }
    }
}

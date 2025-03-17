using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Devolucion
{
    public class DevolucionProductoServicioResponse
    {
        public int IdDevolucion { get; set; }
        public string Descripcion { get; set; }
        public int ServiciosCanales { get; set; }
        public string TipoFacturacion { get; set; }
        public decimal Importe { get; set; }
        public string Glosa { get; set; }
        public string CuentaContable { get; set; }
    }
}

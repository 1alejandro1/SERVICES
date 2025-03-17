using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ServiciosSwamp
{
    public class ServicioCanalResponse
    {
        public int IdServicioCanal { get; set; }
        public string Descripcion { get; set; }
        public string IdModeloBOL { get; set; }
        public string IdModeloUSD { get; set; }
        public string CuentaContable { get; set; }
        public string Producto { get; set; }
        public bool DebitoAbono { get; set; } 
    }
}

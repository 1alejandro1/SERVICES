using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ProductoServicio
{
    public class Producto
    {
        public string IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get;set; }
        public bool ReporteAsfi { get; set; }
    }
}

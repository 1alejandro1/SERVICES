using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ProductoServicio
{
    public class ProductoByTipoEstadoResponse
    {
        public string IdServicio { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string TipoUsuario { get; set; }
        public string TipoAsignacion { get; set; }
    }
}

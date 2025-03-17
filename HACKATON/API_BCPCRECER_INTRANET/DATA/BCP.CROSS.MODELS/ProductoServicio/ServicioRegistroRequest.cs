using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ProductoServicio
{
    public class ServicioRegistroRequest
    {
        public string Descripcion { get; set; }
        public string TipoServicio { get; set; }
        public string Estado { get; set; }
        public string TipoAsignacion { get; set; }
        public string IdProducto { get; set; }
        public string FuncionarioRegistro { get; set; }
        public string DocumentacionRequerida { get; set; }
        public string CodTipoCaso { get; set; }
        public int TiempoResolucion { get; set; }
    }
}

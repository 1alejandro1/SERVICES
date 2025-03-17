using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ProductoServicioResponse
    {
        public string IdProducto { get; set; }
        public string IdServicio { get; set; }
        public string Funcionario { get; set; }
        public string DocumentacionRequerida { get; set; }
        public int Tiempo { get; set; }
        public string Estado { get; set; }
    }
}

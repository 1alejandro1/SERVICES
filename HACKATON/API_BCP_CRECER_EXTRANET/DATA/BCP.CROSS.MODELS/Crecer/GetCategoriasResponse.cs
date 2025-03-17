using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Crecer
{
    public class GetCategoriasResponse
    {
        public List<CategoriasResponse> Categorias { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string codigo { get; set; }
        public string Operacion { get; set; }
    }
}

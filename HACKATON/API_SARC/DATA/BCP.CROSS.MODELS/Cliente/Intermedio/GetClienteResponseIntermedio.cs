using BCP.CROSS.MODELS.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Cliente.Intermedio
{
    public class GetClienteResponseIntermedio
    {
        public GetClienteResponse cliente { get; set; }
        public bool exito { get; set; }
        public string mensaje { get; set; }
        public string codigo { get; set; }
        public string operacion { get; set; }
    }
}

using BCP.CROSS.MODELS.Client;
using System.Collections.Generic;

namespace BCP.CROSS.MODELS.Cliente.Intermedio
{
    public class GetClientesResponseIntermedio
    {
        public List<GetClienteResponse> Clientes { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string codigo { get; set; }
        public string Operacion { get; set; }
    }
}

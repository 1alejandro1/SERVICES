using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Asignacion
{
    public class EstadoCasoRequest
    {
        public string Estado { get; set; }
    }
    public class EstadoCasoFuncionarioRequest : EstadoCasoRequest
    { 
        public string Funcionario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateAsignacionCasoDTO
    {
        public string FuncionarioAtension { get; set; }
        public string FuncionarioModificacion {get; set;}
        public string NroCarta {get; set;}
        public string Estado {get; set;}
        public int TiempoResolucion {get; set;}
        public string Complejidad {get; set;}

    }
}

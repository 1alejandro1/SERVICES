using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateOrigenCasoDTORequest
    {
        public string NroCarta { get; set; }
        public string Area { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Matricula { get; set; }
    }
}

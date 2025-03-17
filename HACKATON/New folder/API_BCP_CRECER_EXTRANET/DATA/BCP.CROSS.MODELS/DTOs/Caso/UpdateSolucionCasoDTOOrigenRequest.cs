using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateSolucionCasoDTOOrigenRequest
    {
        public string NroCarta { get; set; }
        public int Tiempo { get; set; }
        public int Proceso { get; set; }
        public int Riesgo { get; set; }
    }
}

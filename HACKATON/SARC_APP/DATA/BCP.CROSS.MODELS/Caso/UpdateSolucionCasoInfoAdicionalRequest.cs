using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{
    public class UpdateSolucionCasoInfoAdicionalRequest
    {
        public string NroCarta { get; set; }
        public int Tiempo { get; set; }
        public int Proceso { get; set; }
        public int Riesgo { get; set; }
    }
}
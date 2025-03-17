using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ParametrosResponse
    {
        public string EstadoPrincipal { get; set; }
        public int EstadoSecundario { get; set; }
        public string Descripcion { get; set; }
    }
    public class ParametroCardResponse
    {
        public int IdParametro { get; set; }
        public string Descripcion { get; set; }
    }
}

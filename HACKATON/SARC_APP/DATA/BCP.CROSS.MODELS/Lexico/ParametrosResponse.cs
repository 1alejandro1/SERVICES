using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ParametrosResponse
    {
        public string EstadoPrincipal { get; set; }
        public int EstadoSecundario { get; set;}
        public string Descripcion { get; set; }
    }

    public class ParametroSarcResponse
    {
        public int IdParametro { get; set; }
        public string Descripcion { get; set; }
    }
}

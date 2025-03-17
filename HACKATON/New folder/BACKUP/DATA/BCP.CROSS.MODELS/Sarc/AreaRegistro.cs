using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Sarc
{
    public class AreaRegistro
    {
        public string NRO_CARTA { get; set; }
        public string USR_CONSULTA { get; set; }
        public string AREA_CONSULTA { get; set; }
        public string RESPONSABLE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FEC_INI { get; set; }
        public string PRIORIDAD { get; set; }
        public string RESPUESTA { get; set; }
        public int NIVEL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes.CNR
{
    public class TotalModel
    {

        public int SolucionadosTiempo { get; set; } //ST
        public int SolucionadosVencidos { get; set; } // SV
        public int PendientesTiempo { get; set; } // PE
        public int PendientesVencidos { get; set; } //PV
        public int TotalCasos { get; set; }
    }
}

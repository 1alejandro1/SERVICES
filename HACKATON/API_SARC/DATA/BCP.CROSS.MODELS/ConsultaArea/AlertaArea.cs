using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ConsultaArea
{
    public class AlertaArea
    {
        public string NroCaso { get; set; }
        public string Descripcion { get; set; }
        public string Motivo { get; set; }
        public int TiempoAtencioArea { get; set; }
        public int TiempoAtencionSarc { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    public class TipoReclamoModel
    {
        //exec SARC.SP_REP_RECLAMO_SERVICIO @FECHAINI=N'20211128',@FECHAFIN=N'20211215'
        public string Nombre { get; set; } //NOMBRE
        public string Tipo { get; set; } //TIPO
        public int Cantidad { get; set; } // NRO
    }
}

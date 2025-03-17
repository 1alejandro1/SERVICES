using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    //SP=>exec SARC.SP_REP_EXPEDICION @FECHAINI=N'20211128',@FECHAFIN=N'20211215'
    public class ExpedicionModel
    {
        public long Nro { get; set; }
        public string FecEmi { get; set; }//FEC_EMI
        public string FecExp { get; set; } //FEC_EXP
        public string Carta { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Sucursal { get; set; }
        public string Retencion { get; set; }
        public string Usuario { get; set; }
    }
}

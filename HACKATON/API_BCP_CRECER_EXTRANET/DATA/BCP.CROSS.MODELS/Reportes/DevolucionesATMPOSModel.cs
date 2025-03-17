using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    //exec SARC.SP_REP_CREDITO_DEBITO @PRODCREDITO=N'000005',@PRODDEBITO=N'012',@FECHAINI=N'20211128',@FECHAFIN=N'20211215'
    public class DevolucionesATMPOSModel
    {
        public long Nro { get; set; }
        public string Fecha { get; set; }
        public string Telefono { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public string NroCaso { get; set; }
        public string NroTarjeta { get; set; } //NRO_TARJETA
        public string Cliente { get; set; }
        public string Servicio { get; set; }
        public string Observaciones { get; set; }
        public string Llamada1 { get; set; }
        public string Hora1 { get; set; }
        public string Llamada2 { get; set; }
        public string Hora2 { get; set; }
        public string Detalle { get; set; }
        public string Usuario { get; set; }
    }
}

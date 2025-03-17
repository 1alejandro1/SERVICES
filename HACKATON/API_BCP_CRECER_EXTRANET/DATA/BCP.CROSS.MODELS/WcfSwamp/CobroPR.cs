using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.WcfSwamp
{
    public class CobroPR
    {
        public string RutaSharePoint { get; set; }

        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string NroCuenta { get; set; }
        public string ClienteIdc { get; set; }
        public string ClienteIdcTipo { get; set; }
        public string ClienteIdcExtension { get; set; }
        public string DireccionRespuesta { get; set; }
        public string ProductoId { get; set; }
        public string ServicioId { get; set; }
        public string Empresa { get; set; }
        public string Paterno { get; set; }
        public string EmailRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        //request
        public string FuncionarioAtencion { get; set; }//analista funcionario
        public string Supervisor { get; set; }//supervisor
        public string NroCaso { get; set; }
        //cobro dev
        public string DescripcionServicio { get; set; }
        public int ServiciosCanalesId { get; set; }
        //cobro
        public bool FacturacionOnline { get; set; }        
    }
}

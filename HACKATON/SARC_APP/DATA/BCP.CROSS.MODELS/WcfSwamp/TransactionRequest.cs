using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.WcfSwamp
{
    public class TransactionRequest
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

        public string FuncionarioAtencion { get; set; }
        public string Supervisor { get; set; }
        public string NroCaso { get; set; }

        public string DescripcionServicio { get; set; }
        public int ServiciosCanalesId { get; set; }
        public bool FacturacionOnline { get; set; }
        public string ParametroCentro { get; set; }

        public TransactionRequest()
        {
            Moneda = string.Empty;
            NroCuenta = string.Empty;
            ClienteIdc = string.Empty;
            ClienteIdcTipo = string.Empty;
            ClienteIdcExtension = string.Empty;
            DireccionRespuesta = string.Empty;
            ProductoId = string.Empty;
            ServicioId = string.Empty;
            Empresa = string.Empty;
            Paterno = string.Empty;
            EmailRespuesta = string.Empty;
            TelefonoRespuesta = string.Empty;
            FuncionarioAtencion = string.Empty;
            Supervisor = string.Empty;
            NroCaso = string.Empty;
            DescripcionServicio = string.Empty;
        }
    }
}

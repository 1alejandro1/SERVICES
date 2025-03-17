using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{

    public class UpdateDevolucionCobroRequest
    {
        public string NroCarta { get; set; }

        [Display (Name = "Cuenta")]
        public string NroCuentaPR { get; set; }
        public int IdServiciosCanales { get; set; }
        public string ParametroCentroPR { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string TipoFacturacion { get; set; }
        public int IndDevolucionCobro { get; set; }

        public UpdateDevolucionCobroRequest()
        {
            NroCarta = string.Empty;
            NroCuentaPR = string.Empty;
            ParametroCentroPR = string.Empty;
            Moneda = string.Empty;
            TipoFacturacion = string.Empty;
        }
    }
}
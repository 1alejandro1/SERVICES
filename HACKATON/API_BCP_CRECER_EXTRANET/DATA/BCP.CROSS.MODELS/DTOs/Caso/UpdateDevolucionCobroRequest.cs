using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateDevolucionCobroRequest
    {
        public string NroCarta { get; set; }
        public string NroCuentaPR { get; set; }
        public int IdServiciosCanales { get; set; }
        public string ParametroCentroPR { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string TipoFacturacion { get; set; }
        public int IndDevolucionCobro { get; set; }
    }
}

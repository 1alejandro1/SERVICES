using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class FlujoCasoExpressDTORequest
    {
        public int StatusCode { get; set; }
        public string NroCaso { get; set; }
        public string RutaSharepoint { get; set; }
        public bool esCobro { get; set; }
        public bool esDevolucion { get; set; }
        public CreateandSolveCasoDTOV2 Caso { get; set; }
        public FlujoCasoExpressDTODevolucionData Devolucion { get; set; }
        public FlujoCasoExpressDTOCobroData Cobro { get; set; }
    }
    
    public class FlujoCasoExpressDTODevolucionData
    {
        public int ServiciosCanalesId { get; set; }
        public string ParametroCentro { get; set; }
        public string DescripcionServicio { get; set; }
    }
    public class FlujoCasoExpressDTOCobroData
    {
        public int ServiciosCanalesId { get; set; }     
        public string DescripcionServicio { get; set; }
        public string TipoFacturacion { get; set; }//online("O") o batch("B")
    }
}

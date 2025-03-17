using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{

    public class CobroRequest
    {
        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Formato invalido")]
        [Display(Name = "Monto a Cobrar")]
        public string Monto { get; set; }
        
        [Required]
        public string Moneda { get; set; }

        [Required]
        [Display(Name = "Cuenta Cobro")]
        public string CuentaCobro { get; set; }

        [Required]
        [Display(Name = "Cuenta contable")]
        public int ServiciosCanalesId { get; set; }

        [Required]
        [Display(Name = "Centro de costo")]
        public string ParametroCentro { get; set; }

        [Required]
        [Display(Name = "Descripción Servicio")]
        public string DescripcionServicio { get; set; }

        [Required]
        [Display(Name = "Tipo Facturacion")]
        public string TipoFacturacion { get; set; }
    }


}
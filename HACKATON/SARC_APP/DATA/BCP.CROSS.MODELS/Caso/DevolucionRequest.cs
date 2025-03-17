using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{

    public class DevolucionRequest
    {
        [Display(Name = "Monto a devolver")]
        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Formato invalido")]
        [Remote(action: "ValidaMontoDevolucion", controller: "Casos", AdditionalFields = nameof(Moneda))]
        public string Monto { get; set; }

        [Required]
        public string Moneda { get; set; }

        [Required]
        [Display(Name = "Cuenta devolución")]
        [Remote(action: "ValidaLimiteDevolucionCuenta", controller: "Casos")]
        public string CuentaDevolucion { get; set; }

        [Required]
        [Display(Name = "Centro de costo")]
        public string ParametroCentro { get; set; }

        [Required]
        [Display(Name = "Cuenta contable")]
        public int ServiciosCanalesId { get; set; }

        [Required]
        [Display(Name = "Descripción Servicio")]
        public string DescripcionServicio { get; set; }
    }
}
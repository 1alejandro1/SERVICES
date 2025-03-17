using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS.Caso
{
    public class CreateCasoExpressDTO : CreateCasoDTO
    {
        [Required]
        [Display(Name = "Tipo Solución")]
        public string TipoSolucion { get; set; }

        [Required]
        [Display(Name = "Descripción Solución")]
        public string DescripcionSolucion { get; set; }

        public CreateCasoExpressDTO()
        {
            MonedaDevolucion = string.Empty;
            InformacionAdicional = string.Empty;
            AtmSucursal = string.Empty;
            AtmUbicacion = string.Empty;
            DocumentosAdjuntoIn = string.Empty;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace BCP.CROSS.MODELS.Caso
{
    public class UpdateCasoSolucionRequest
    {
        public string FuncionarioModificacion {get; set;}

        [Required]
        [Display(Name = "Nro Caso")]
        public string NroCarta{ get; set; }
        public string Estado {get; set;}

        [Required]
        [Display(Name = "Tipo Solucion")]
        public string TipoSolucion {get; set;}

        [Required]
        [Display(Name = "Descripción Solucion")]
        public string DescripcionSolucion {get; set;}
        public string SucursalSolucion {get; set;}

        [Display(Name = "Documentación Adjunta")]
        public string DocumentoAdjuntoOut {get; set;}

        [Display(Name = "Tipo Carta")]
        public string TipoCarta {get; set;}

        [Display(Name = "Monto")]
        public decimal ImporteDevolucion {get; set;}

        [Display(Name ="Moneda")]
        public string MonedaDevolucion {get; set;}


        public UpdateCasoSolucionRequest()
        {
            FuncionarioModificacion = string.Empty;
            NroCarta = string.Empty;
            Estado = string.Empty;
            TipoSolucion = string.Empty;
            DescripcionSolucion = string.Empty;
            SucursalSolucion = string.Empty;
            DocumentoAdjuntoOut = string.Empty;
            TipoCarta = string.Empty;
            MonedaDevolucion = string.Empty;
        }
    }
}
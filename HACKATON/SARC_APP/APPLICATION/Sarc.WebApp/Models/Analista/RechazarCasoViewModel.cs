using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sarc.WebApp.Models.Analista
{

    public class RechazarCasoViewModel
    {
        public string FechaRegistro { get; set; }
        public string FuncionarioRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NroCaso { get; set; }
        public string Estado { get; set; }
        public string AntServ { get; set; }

        [Display(Name ="Producto")]
        [Required]
        public string ProductoId { get; set; }

        [Display(Name ="Servicio")]
        [Required]
        public string ServicioId { get; set; }

        [Display(Name ="Tipo Caso")]
        public string TipoServicio { get; set; }
        public string Documentacion { get; set; }

        [Display(Name ="Error de tipo caso?")]
        public string SWErrorReg { get; set; }

        [Display(Name ="Tipo de Error")]
        [Required]
        public string IdRegistroError { get; set; }

        [Display(Name ="Observación")]
        [Required]
        public string DescripcionRegistroError { get; set; }

        public IEnumerable<SelectListItem> ProductoDropDown { get; set; }
        public IEnumerable<SelectListItem> ServicioDropDown { get; set; }
        public IEnumerable<SelectListItem> TipoErrorDropDown { get; set; }

    }


}
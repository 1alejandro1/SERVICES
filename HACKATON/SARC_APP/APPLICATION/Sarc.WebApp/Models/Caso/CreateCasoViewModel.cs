using Microsoft.AspNetCore.Mvc.Rendering;
using BCP.CROSS.MODELS.Client;
using System.ComponentModel.DataAnnotations;
using BCP.CROSS.MODELS.Caso;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace SARCAPP.APPLICATION.Sarc.WebApp.Models.Caso
{

    public class CreateCasoViewModel
    {
        public IEnumerable<SelectListItem> AtmUbicacionDropDown { get; set; }
        public IEnumerable<SelectListItem> AtmSucursalDropDown { get; set; }
        public IEnumerable<SelectListItem> DepartamentoDropDown { get; set; }
        public IEnumerable<SelectListItem> CiudadDropDown { get; set; }
        public IEnumerable<SelectListItem> ProductoDropDown { get; set; }
        public IEnumerable<SelectListItem> ServicioDropDown { get; set; }
        public IEnumerable<SelectListItem> ViaEnvioCodigoDropDown { get; set; }
        public GetClienteResponse GetClienteResponse{ get; set; }

        [Required]
        [Display(Name = "Fecha TXN")]
        [DataType(DataType.DateTime)]
        public DateTime FechaTxn { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));

        public string Accion { get; set; }
        
        [Display(Name = "Firma verificada?")]
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Confirme la verificación de la firma")]
        public bool FirmaVerificada { get; set; }

        [Display(Name = "Adjuntar Archivos")]
        public List<IFormFile> Files { get; set; }
        public CreateCasoDTO Caso { get; set; }
    }
}
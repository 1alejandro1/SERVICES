using Microsoft.AspNetCore.Mvc.Rendering;
using BCP.CROSS.MODELS.Client;
using System.ComponentModel.DataAnnotations;
using BCP.CROSS.MODELS.Caso;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SARCAPP.APPLICATION.Sarc.WebApp.Models.Caso
{

    public class CreateCasoExpressViewModel
    {
        public IEnumerable<SelectListItem> AtmUbicacionDropDown { get; set; }
        public IEnumerable<SelectListItem> AtmSucursalDropDown { get; set; }
        public IEnumerable<SelectListItem> DepartamentoDropDown { get; set; }
        public IEnumerable<SelectListItem> CiudadDropDown { get; set; }
        public IEnumerable<SelectListItem> ProductoDropDown { get; set; }
        public IEnumerable<SelectListItem> ServicioDropDown { get; set; }
        public IEnumerable<SelectListItem> ViaEnvioCodigoDropDown { get; set; }
        
        public IEnumerable<SelectListItem> TipoSolucionDropDown { get; set; }
        public IEnumerable<SelectListItem> CuentaContableDropDown { get; set; }
        public IEnumerable<SelectListItem> CentroDeCostoDropDown { get; set; }
        public GetClienteResponse GetClienteResponse{ get; set; }

        [Required]
        [Display(Name = "Fecha TXN")]
        [DataType(DataType.DateTime)]
        public DateTime FechaTxn { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));


        [Display(Name = "Cobro")]
        public bool EsCobro { get; set; }
        
        [Display(Name = "Devolución")]
        public bool EsDevolucion { get; set; }

        public string Accion { get; set; }

        [Display(Name = "Asignado")]
        [Remote(action: "ValidaLimiteDevolucionAnalista", controller: "Casos", AdditionalFields = nameof(Accion))]
        public string Analista { get; set; }

        [Display(Name = "Firma verificada?")]
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Confirme la verificación de la firma")]
        public bool FirmaVerificada { get; set; }

        [Display(Name = "Adjuntar Archivos")]
        public List<IFormFile> Files { get; set; }
        public CreateCasoExpressDTO Caso { get; set; }
        public DevolucionRequest Devolucion { get; set; }
        public CobroRequest Cobro { get; set; }
    }
}
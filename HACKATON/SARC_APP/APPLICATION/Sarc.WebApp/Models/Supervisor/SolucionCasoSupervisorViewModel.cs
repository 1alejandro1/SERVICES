using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BCP.CROSS.MODELS.Caso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sarc.WebApp.Models.Supervisor
{
    public class SolucionCasoSupervisorViewModel
    {
        public IEnumerable<SelectListItem> ProductoDropDown { get; set; }
        public IEnumerable<SelectListItem> ServicioDropDown { get; set; }
        public IEnumerable<SelectListItem> AreaDropDown { get; set; }
        public IEnumerable<SelectListItem> TipoSolucionDropDown { get; set; }
        public IEnumerable<SelectListItem> CuentaContableDropDown { get; set; }
        public IEnumerable<SelectListItem> CentroDeCostoDropDown { get; set; }
        public IEnumerable<SelectListItem> CartaDropDown { get; set; }
        public IEnumerable<SelectListItem> RiesgoDropDown { get; set; }
        public IEnumerable<SelectListItem> TiempoDropDown { get; set; }
        public IEnumerable<SelectListItem> ProcesoDropDown { get; set; }
        public IEnumerable<SelectListItem> TipoErrorDropDown { get; set; }
        public List<SelectListItem> ViaRespuestaBtns { get; set; }
        public List<SelectListItem> AccionBtns { get; set; }
        public UpdateSolucionCasoInfoAdicionalRequest InfoAdicional { get; set; }
        public UpdateOrigenCasoRequest OrigenCaso { get; set; }

        public string FuncionarioAtencion { get; set; }
        public string Accion { get; set; }

        public CasoDTOAll Caso { get; set; }
        public UpdateCasoSolucionRequest UpdateCaso { get; set; }
        public UpdateCasoViaEnvio UpdateRespuesta { get; set; }
        public CerrarCasoRequest CerrarCaso { get; set; }
        public UpdateDevolucionCobroRequest DevCob { get; set; }
        public UpdateCasoRechazarDTO RechazarCaso { get; set; }

        [Display(Name = "Adjuntar Archivos")]
        public List<IFormFile> Files { get; set; }
        public DevolucionRequest Devolucion { get; set; }
        public CobroRequest Cobro { get; set; }
        
    }


}
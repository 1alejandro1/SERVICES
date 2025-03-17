using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Generated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Card_App.Models.Generated
{
    public class GeneratedViewModel
    {
        public GeneratedResponse Registro { get; set; }
        public int CantRegistro { get; set; }
        public int CantErrorRegistro { get; set; }
        public GeneratedRequest Generated { get; set; }
        public IEnumerable<SelectListItem> TiposIdcDropDown { get; set; }        
        public List<IFormFile> Files { get; set; }        
    }
}

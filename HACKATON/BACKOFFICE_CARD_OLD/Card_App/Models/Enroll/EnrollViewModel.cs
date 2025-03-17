using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Enroll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Card_App.Models.Enroll
{
    public class EnrollViewModel
    {        
        public EnrollRequest Enroll { get; set; }
        public EnrollDTO EnrollView { get; set; }
        public IEnumerable<SelectListItem> CategoriasDropDown { get; set; }
        public ServiceResponse<bool?> EnrollResponse { get; set; }
    }
}

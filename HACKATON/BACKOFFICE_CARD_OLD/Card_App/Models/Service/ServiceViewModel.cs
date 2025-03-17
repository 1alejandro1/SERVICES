using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Commerce;
using BCP.CROSS.MODELS.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Card_App.Models.Service
{
    public class ServiceViewModel
    {
        public ListServiceResponse ServiceResponse { get; set; }
        public IEnumerable<SelectListItem> CommerceDropDown { get; set; }
        public CommerceDTO CommerceDTO { get; set; }
        public CommerceResponse CommerceResponse { get; set; }
    }
}

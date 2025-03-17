using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sarc.WebApp.Models.Clients
{
    public class ClientViewModel
    {
        public GetClientByIdcRequest ByIdcRequest { get; set; }
        public GetClientByDbcRequest ByDbcRequest { get; set; }
        public IEnumerable<SelectListItem> TiposIdcDropDown { get; set; }
        public ServiceResponse<IEnumerable<GetClienteResponse>> ClientResponse { get; set; }
    }
}

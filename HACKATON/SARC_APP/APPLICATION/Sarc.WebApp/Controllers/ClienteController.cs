using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models.Clients;
using System.Linq;
using System.Threading.Tasks;
using BCP.CROSS.COMMON.Helpers;
using System.Collections.Generic;

namespace Sarc.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REG")]
    public class ClienteController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILexicoService _lexicoService;

        public ClienteController(IClientService clientService, ILexicoService lexicoService)
        {
            _clientService = clientService;
            _lexicoService = lexicoService;
        }

        public async Task<IActionResult> BusquedaCliente()
        {
            HttpContext.Session.Remove("nrocaso");
            HttpContext.Session.Remove("pdfcaso");
            ClientViewModel clientViewModel = new();
            clientViewModel.TiposIdcDropDown = await GetIdc();

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BusquedaCliente(ClientViewModel clientViewModel)
        {

            if (!string.IsNullOrEmpty(clientViewModel?.ByIdcRequest?.Idc))
            {
                var validateModel = TryValidateModel(clientViewModel.ByIdcRequest);
                clientViewModel.TiposIdcDropDown = await GetIdc(clientViewModel.ByIdcRequest.TipoIdc);
                if (!validateModel)
                {
                    return View(clientViewModel);
                }
                clientViewModel.ClientResponse = await _clientService.GetClientsByIdcAsync(clientViewModel.ByIdcRequest);
                ModelState.ClearValidationState(nameof(clientViewModel.ByIdcRequest));
                return View(clientViewModel);
            }
            else if (!string.IsNullOrEmpty(clientViewModel?.ByDbcRequest?.PaternoRazonSocial))
            {
                var validateModel = TryValidateModel(clientViewModel.ByDbcRequest);
                clientViewModel.TiposIdcDropDown = await GetIdc();
                if (!validateModel)
                {
                    return View(clientViewModel);
                }
                clientViewModel.ClientResponse = await _clientService.GetClientsByDbcAsync(clientViewModel.ByDbcRequest);
                ModelState.ClearValidationState(nameof(clientViewModel.ByDbcRequest));
                return View(clientViewModel);
            }

            return View();
        
        }

        private async Task<IEnumerable<SelectListItem>> GetIdc(string idcSelected = "")
        {
            var idcs = await _lexicoService.GetTipoIdc();
           return idcs.Select(x => new SelectListItem(x.Descripcion, x.Tipo, x.Tipo.Equals(idcSelected)? true:false));
        }
    }
}

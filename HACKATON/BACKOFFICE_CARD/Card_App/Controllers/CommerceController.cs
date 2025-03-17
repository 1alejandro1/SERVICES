using BCP.CROSS.MODELS.Commerce;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Card_App.Models.Commerce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Card_App.Controllers
{
    public class CommerceController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private readonly ICommerceService _commerceService;
        private readonly string PublicToken;
        private readonly string AppUserId;
        private readonly string Channel;
        public CommerceController(IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IManagerSecrypt secrypt, ICommerceService commerceService)
        {
            _logger = logger;
            _authService = authService;
            _securitySettigns = securitySettigns;
            _secrypt = secrypt;
            _commerceService = commerceService;
            PublicToken = _secrypt.Desencriptar(_securitySettigns.Value.PublicToken);
            AppUserId = _securitySettigns.Value.AppUserId;
        }
        public async Task<IActionResult> Commerce()
        {
            try
            {

            ListAllCommerceRequest request = new ListAllCommerceRequest();
            request.publicToken = PublicToken;
            request.appUserId = AppUserId;
            CommerceViewModel CommerceViewModel = new();

            var CommerceResponse = await _commerceService.ObtieneListCommerceAsync(request);

            CommerceViewModel.CommerceResponse = CommerceResponse;

            return View(CommerceViewModel);

            }
            catch(Exception ex)
            {
                _logger.Error("Error al obtener comercios Controller Commerce(): ", ex);
                return View(null);
            }
        }
    }
}

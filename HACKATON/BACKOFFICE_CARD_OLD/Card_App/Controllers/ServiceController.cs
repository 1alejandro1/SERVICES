using BCP.CROSS.MODELS.Service;
using BCP.CROSS.MODELS.Commerce;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Card_App.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Card_App.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILexicoService _lexicoService;
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private readonly IServicesService _servicesService;
        private readonly string PublicToken;
        private readonly string AppUserId;
        private readonly string Channel;
        public ServiceController(IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IManagerSecrypt secrypt, IServicesService servicesService, ILexicoService lexicoService)
        {
            _lexicoService = lexicoService;
            _logger = logger;
            _authService = authService;
            _securitySettigns = securitySettigns;
            _secrypt = secrypt;
            _servicesService = servicesService;
            PublicToken = _secrypt.Desencriptar(_securitySettigns.Value.PublicToken);
            AppUserId = _securitySettigns.Value.AppUserId;
            _lexicoService = lexicoService;
        }
        public async Task<IActionResult> Service()
        {
            ServiceViewModel ServiceViewModel = new();
            ListServiceRequest request = new ListServiceRequest();
            ListAllCommerceRequest requestCommerce = new ListAllCommerceRequest();
            requestCommerce.publicToken = PublicToken;
            requestCommerce.appUserId = PublicToken;
            request.publicToken = PublicToken;
            request.appUserId = AppUserId;
            request.idCommerce = 1;

            var Comercios = await _lexicoService.ObtieneListCommerceAsync(requestCommerce);

        
                ServiceViewModel.CommerceDropDown = Comercios.data.commerces.Select(x => new SelectListItem(x.name, x.nit));
              

            var servicios = await _servicesService.ObtieneListServiceAsync(request);

            ServiceViewModel.ServiceResponse = servicios.data;

            return View(ServiceViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Service(ServiceViewModel ServiceViewModel)
        {
            ListServiceRequest request = new ListServiceRequest();
            
            request.publicToken = PublicToken;
            request.appUserId = AppUserId;
            request.idCommerce = ServiceViewModel.CommerceDTO.id;

            var servicios = await _servicesService.ObtieneListServiceAsync(request);

            ServiceViewModel.ServiceResponse = servicios.data;

            return View(ServiceViewModel);
        }
    }
}

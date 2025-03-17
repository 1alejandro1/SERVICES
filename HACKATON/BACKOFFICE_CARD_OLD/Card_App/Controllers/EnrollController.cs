using BCP.CROSS.MODELS.Category;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Card_App.Models.Enroll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UAParser;

namespace Card_App.Controllers
{
    public class EnrollController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEnrollService _enrollService;
        private readonly ILoggerManager _logger;
        private readonly ILexicoService _lexicoService;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private HttpClient _httpClient;
        private readonly IManagerSecrypt _secrypt;
        private readonly string PublicToken;
        private readonly string AppUserId;
        private readonly string Channel;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EnrollController(IWebHostEnvironment hostEnvironment, IEnrollService enrollService, IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IHttpClientFactory httpClientFactory, IManagerSecrypt secrypt, ILexicoService lexicoService)
        {
            _enrollService = enrollService;
            _authService = authService;
            _logger = logger;
            _securitySettigns = securitySettigns;
            _secrypt = secrypt;
            _lexicoService = lexicoService;
            IHttpClientFactory _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("CARD_API");
            PublicToken = _secrypt.Desencriptar(_securitySettigns.Value.PublicToken);
            AppUserId = _securitySettigns.Value.AppUserId;
            Channel = _securitySettigns.Value.Channel;
            _hostingEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Enroll()
        {
            ListAllCategoryRequest ListCategoryRequest = new ListAllCategoryRequest();
            EnrollViewModel enrollViewModel = new();
            ListCategoryRequest.publicToken = PublicToken;
            ListCategoryRequest.appUserId = AppUserId;
            var Categorias = await _lexicoService.ObtieneListCategoryAsync(ListCategoryRequest);
         
                enrollViewModel.CategoriasDropDown = Categorias.data.categories.Select(x => new SelectListItem(x.name, x.id.ToString()));         

            return View(enrollViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollViewModel request)
        {
               var user = await _authService.GetToken();

            request.Enroll.publicToken = PublicToken;
            request.Enroll.appUserId = AppUserId;
            foreach(var item in request.Enroll.commerce)
            {
                item.name = request.EnrollView.name?.Trim();
                item.commerceCode = request.EnrollView.commerceCode?.Trim();
                item.businessName = request.EnrollView.businessName?.Trim();
                item.nit = request.EnrollView.nit?.Trim();
                item.phoneNumber = request.EnrollView.phoneNumber?.Trim();
                item.city = request.EnrollView.city?.Trim();
                item.logoUrl = request.EnrollView.logoUrl?.Trim();
                item.socialNetworkLink = request.EnrollView.socialNetworkLink?.Trim();
            }
            request.Enroll.categoryId = request.EnrollView.categoryId;
            foreach (var item in request.Enroll.serviceEnroll)
            {
                item.serviceCode = request.EnrollView.serviceCode?.Trim();
                item.service = request.EnrollView.service?.Trim();
                item.description = request.EnrollView.description?.Trim();
                item.startDate = request.EnrollView.startDate;
                item.endDate = request.EnrollView.endDate;
                item.discountText = request.EnrollView.discountText?.Trim();
                item.discountAmount = request.EnrollView.discountAmount;
                item.maximumAmount = request.EnrollView.maximumAmount;
                item.discountRate = request.EnrollView.discountRate;
                item.disclaimer = request.EnrollView.disclaimer?.Trim();
                item.stock = request.EnrollView.stock?.Trim();
                item.type = request.EnrollView.type?.Trim();
                item.serviceCity = request.EnrollView.serviceCity?.Trim();
                item.largeImageUrl = request.EnrollView.largeImageUrl?.Trim();
                item.smallImageUrl = request.EnrollView.smallImageUrl?.Trim();
                item.template = request.EnrollView.template?.Trim();
                item.codeType = request.EnrollView.codeType?.Trim();
                item.formatCode = request.EnrollView.formatCode?.Trim();
                item.axisX = request.EnrollView.axisX;
                item.codeWidth = request.EnrollView.codeWidth;
                item.codeHigh = request.EnrollView.codeHigh;
                item.marginCode = request.EnrollView.marginCode;
                item.encryption = request.EnrollView.encryption?.Trim();
                item.serviceState = request.EnrollView.serviceState?.Trim();
            }
            request.Enroll.domainUser = user.matricula;

            _logger.Information($"Request: {JsonSerializer.Serialize(request)}");
            var response = await _enrollService.Enroll(request.Enroll);
            _logger.Information($"Response: {JsonSerializer.Serialize(response)}");

            request.EnrollResponse = response;

            return RedirectToAction("Generated");

        }
    }
}

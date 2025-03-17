using BCP.CROSS.MODELS.Generated;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Card_App.Models.Generated;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
    public class GeneratedController : Controller
    {
        private readonly IGeneratedService _generatedService;
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;
        private readonly ILexicoService _lexicoService;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private HttpClient _httpClient;
        private readonly IManagerSecrypt _secrypt;
        private readonly string PublicToken;
        private readonly string AppUserId;
        private readonly string Channel;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public GeneratedController(IWebHostEnvironment hostEnvironment,IGeneratedService generatedService, IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IHttpClientFactory httpClientFactory, IManagerSecrypt secrypt, ILexicoService lexicoService)
        {
            _generatedService = generatedService;
            _logger = logger;
            _authService = authService;
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
        public async Task<IActionResult> Generated()
        {
            GeneratedViewModel generatedViewModel = new();
            generatedViewModel.TiposIdcDropDown = await GetIdc();

            return View(generatedViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Generated(GeneratedViewModel request)
        {
            var user = await _authService.GetToken();
         
                request.Generated.ArchivosAdjuntos = new List<ArchivoAdjunto>();
                request.Generated.publicToken = PublicToken;
                request.Generated.appUserId = AppUserId;
                request.Generated.documentNumber = request.Generated.documentNumber?.Trim();
                request.Generated.documentType = request.Generated.documentType?.Trim();
                request.Generated.documentExtention = request.Generated.documentExtention?.Trim();
                request.Generated.documentComplement = request.Generated.documentComplement != null ? request.Generated.documentComplement : "";                
                request.Generated.cic = formatCIC(request.Generated.cic.Trim());
                request.Generated.name = request.Generated.name?.Trim();
                request.Generated.lastName = request.Generated.lastName?.Trim();
                request.Generated.secondName = request.Generated.secondName?.Trim();
                request.Generated.accountNumber = request.Generated.accountNumber?.Trim();
            request.Generated.accountType = "3";
            request.Generated.accountDescription = "SOL";
                request.Generated.cardCommerceCode = request.Generated.cardCommerceCode?.Trim();
                request.Generated.cardCommerceId = request.Generated.cardCommerceId?.Trim();
                request.Generated.serviceId = request.Generated.serviceId;
                request.Generated.cellPhoneNumber = request.Generated.cellPhoneNumber?.Trim();
                request.Generated.email = request.Generated.email?.Trim();
            request.Generated.dateBirth = formatFecha(request.Generated.dateBirth.Trim());
                request.Generated.address = request.Generated.address?.Trim();
                request.Generated.gender = request.Generated.gender?.Trim();
            request.Generated.activationDate = formatFecha(request.Generated.activationDate.Trim());
            request.Generated.expirationDate = formatFecha(request.Generated.expirationDate.Trim());
                if (request.Generated.cardStatus == "true")
                {
                    request.Generated.cardStatus = "A";
                }
                else
                {
                    request.Generated.cardStatus = "N";
                }
                request.Generated.cardName = request.Generated.cardName?.Trim();
                request.Generated.ip = getIP();
                request.Generated.identifier = "dc46509d2ef238c3";
                request.Generated.operatingSystem = getOS();
                request.Generated.device = getDevice();
                request.Generated.browser = getBrowser();
                request.Generated.channel = Channel;
                request.Generated.domainUser = user.matricula;

                _logger.Information($"Request: {JsonSerializer.Serialize(request)}");
                var response = await _generatedService.Generated(request.Generated);
                _logger.Information($"Response: {JsonSerializer.Serialize(response)}");


            if (request.Generated.cardStatus == "A")
            {
                request.Generated.cardStatus = "true";
            }
            else
            {
                request.Generated.cardStatus = "false";
            }
            request.Registro = response;

                return View(request);
          
        }
    
        private async Task<IEnumerable<SelectListItem>> GetIdc(string idcSelected = "")
        {
            var idcs = await _lexicoService.GetTipoIdc();
            return idcs.Select(x => new SelectListItem(x.Descripcion, x.Tipo, x.Tipo.Equals(idcSelected) ? true : false));
        }
        private string formatCIC(string cic)
        {
            while (cic.Length < 8)
            {
                cic = "00" + cic;
            }

            return cic;
        }
        private string getIP()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }

            return IPAddress != null ? IPAddress : "0.0.0.0" ;
        }
        private string getOS()
        {            
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }
        private string getBrowser()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c != null ? c.UserAgent.ToString() : "Unknow";
        }
        private string formatFecha(string fechaString)
        {
            if (fechaString == null || fechaString == "")
            {
                return string.Empty;
            }
            else
            {
                DateTime fecha = new DateTime();
                fecha = Convert.ToDateTime(fechaString);
                fechaString = fecha.ToString("yyyy-MM-dd");
                return fechaString;
            }

        }
        private string getDevice()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c != null ? c.Device.Family.ToString() : "Unknow";
        }
      
    }
}

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
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UAParser;

namespace Card_App.Controllers
{
    public class CargaGeneratedController : Controller
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
        public int CountSuccess = 0;
        public int CountError = 0;
        private bool validateEmail = false;
        public CargaGeneratedController(IWebHostEnvironment hostEnvironment, IGeneratedService generatedService, IAuthService authService, ILoggerManager logger, IOptions<SecryptOptions> securitySettigns, IHttpClientFactory httpClientFactory, IManagerSecrypt secrypt, ILexicoService lexicoService)
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
        public async Task<IActionResult> CargaGenerated()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CargaGenerated(GeneratedViewModel request)
        {
            var user = await _authService.GetToken();
            GeneratedResponse validateResponse = new GeneratedResponse();
                request.Generated.ArchivosAdjuntos = ArchivosAdjuntos(request.Files);
            foreach (var item in request.Generated.ArchivosAdjuntos)
            {
                var requestDocument = await GetDataDocument(item);

                foreach (var itemValidate in requestDocument)
                {
                    validateEmail = IsValidEmail(itemValidate.email.Trim().ToString());
                    if (validateEmail)
                    {
                        CountSuccess++;
                    }
                    else
                    {
                        CountError++;
                    }
                }
                if (CountError > 0)
                {
                    validateResponse.message = "Error en validación de formato : Se encontraron " + CountError.ToString() + " registros con el formato de email incorrecto, por favor corrijalos y vuelva a cargar el documento";

                    request.Registro = validateResponse;
                }
                else
                {
                    foreach (var itemRequest in requestDocument)
                    {
                        request.Generated.ArchivosAdjuntos = new List<ArchivoAdjunto>();
                        request.Generated.publicToken = PublicToken;
                        request.Generated.appUserId = AppUserId;
                        request.Generated.documentNumber = itemRequest.documentNumber?.Trim();
                        request.Generated.documentType = "Q";
                        request.Generated.documentExtention = itemRequest.documentExtention?.Trim();
                        request.Generated.documentComplement = itemRequest.documentComplement?.Trim() != null ? itemRequest.documentComplement?.Trim() : "";
                        request.Generated.cic = formatCIC(itemRequest.cic.Trim());
                        request.Generated.name = itemRequest.name?.Trim();
                        request.Generated.lastName = itemRequest.lastName?.Trim();
                        request.Generated.secondName = itemRequest.secondName?.Trim();
                        request.Generated.accountNumber = itemRequest.accountNumber?.Trim();
                        request.Generated.accountType = "3";
                        request.Generated.accountDescription = "SOL";
                        request.Generated.cardCommerceCode = itemRequest.cardCommerceCode?.Trim();
                        request.Generated.cardCommerceId = itemRequest.cardCommerceId?.Trim();
                        request.Generated.serviceId = itemRequest.serviceId;
                        request.Generated.cellPhoneNumber = itemRequest.cellPhoneNumber?.Trim();
                        request.Generated.email = itemRequest.email?.Trim();
                        request.Generated.dateBirth = formatFecha(itemRequest.dateBirth.Trim());
                        request.Generated.address = itemRequest.address?.Trim();
                        request.Generated.gender = itemRequest.gender?.Trim();
                        request.Generated.activationDate = formatFecha(itemRequest.activationDate.Trim());
                        request.Generated.expirationDate = formatFecha(itemRequest.expirationDate.Trim());
                        request.Generated.cardStatus = "A";
                        request.Generated.cardName = itemRequest.cardName?.Trim();
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
                        if (response.message != "COMPLETADO")
                        {
                            CountError = CountError + 1;
                        }
                        else
                        {
                            CountSuccess = CountSuccess + 1;
                        }
                        request.Registro = response;
                    }
                }
            }
            
            request.CantRegistro = CountSuccess;
            request.CantErrorRegistro = CountError;

                return View(request);
           
        }
        private async Task<List<GeneratedRequest>> GetDataDocument(ArchivoAdjunto adjunto)
        {
            var bytes = Convert.FromBase64String(adjunto.Archivo);
            var contents = new StreamContent(new MemoryStream(bytes));

            //export the excel file to a temp file.
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "files/" + adjunto.Nombre);
            System.IO.File.WriteAllBytes(filePath, bytes);


            //Read the connection string for the Excel file.
            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            List<GeneratedRequest> generatedList = new List<GeneratedRequest>();

            //import the excel data.
            try
            {
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.  
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.  
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            bool result = System.IO.File.Exists(filePath);
            if (result == true)
            {
                System.IO.File.Delete(filePath);
            }
            else
            {

            }

            //loop through the datatable and convert it to list.
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GeneratedRequest tarjeta = new GeneratedRequest();
                    tarjeta.documentNumber = row["documentNumber"].ToString()?.Trim();
                    //tarjeta.documentType = row["documentType"].ToString()?.Trim();
                    tarjeta.documentExtention = row["documentExtention"].ToString()?.Trim();
                    tarjeta.documentComplement = row["documentComplement"].ToString()?.Trim();
                    tarjeta.cic = row["cic"].ToString()?.Trim();
                    tarjeta.name = row["name"].ToString()?.Trim();
                    tarjeta.lastName = row["lastName"].ToString()?.Trim();
                    tarjeta.secondName = row["secondName"].ToString()?.Trim();
                    tarjeta.accountNumber = row["accountNumber"].ToString()?.Trim();
                    //tarjeta.accountType = row["accountType"].ToString();
                    //tarjeta.accountDescription = row["accountDescription"].ToString()?.Trim();
                    tarjeta.cardCommerceCode = row["cardCommerceCode"].ToString()?.Trim();
                    tarjeta.cardCommerceId = row["cardCommerceId"].ToString()?.Trim();
                    tarjeta.serviceId = Convert.ToInt32(row["serviceId"]);
                    tarjeta.cellPhoneNumber = row["cellPhoneNumber"].ToString()?.Trim();
                    tarjeta.email = row["email"].ToString()?.Trim();
                    tarjeta.dateBirth = row["dateBirth"].ToString()?.Trim();
                    tarjeta.address = row["address"].ToString()?.Trim();
                    tarjeta.gender = row["gender"].ToString()?.Trim();
                    tarjeta.activationDate = row["activationDate"].ToString()?.Trim();
                    tarjeta.expirationDate = row["expirationDate"].ToString()?.Trim();
                    //tarjeta.cardStatus = row["cardStatus"].ToString()?.Trim();
                    tarjeta.cardName = row["cardName"].ToString()?.Trim();
                    //tarjeta.ip = row["ip"].ToString()?.Trim();
                    //tarjeta.identifier = row["identifier"].ToString()?.Trim();
                    //tarjeta.operatingSystem = row["operatingSystem"].ToString()?.Trim();
                    //tarjeta.device = row["device"].ToString()?.Trim();
                    //tarjeta.browser = row["browser"].ToString()?.Trim();
                    generatedList.Add(tarjeta);
                }
            }


            return generatedList;
        }
        private async Task<IEnumerable<SelectListItem>> GetIdc(string idcSelected = "")
        {
            var idcs = await _lexicoService.GetTipoIdc();
            return idcs.Select(x => new SelectListItem(x.Descripcion, x.Tipo, x.Tipo.Equals(idcSelected) ? true : false));
        }
        private string formatFecha (string fechaString)
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
        private string formatCIC (string cic)
        {
            while(cic.Length < 8)
            {
                cic = "00"+cic;
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

            return IPAddress != null ? IPAddress : "0.0.0.0";
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
        private string getDevice()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c != null ? c.Device.Family.ToString() : "Unknow";
        }
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private static List<ArchivoAdjunto> ArchivosAdjuntos(List<IFormFile> file)
        {
            List<ArchivoAdjunto> adjuntos = new();
            if (file is null)
            {
                return adjuntos;
            }

            long sizeFiles = file.Sum(x => x.Length);
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    formFile.CopyTo(ms);
                    var fileByte = ms.ToArray();
                    var adj = new ArchivoAdjunto
                    {
                        Archivo = Convert.ToBase64String(fileByte),
                        Nombre = formFile.FileName
                    };
                    adjuntos.Add(adj);
                }
            }

            return adjuntos;
        }
    }
}

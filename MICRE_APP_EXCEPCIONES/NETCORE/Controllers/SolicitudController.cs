using MICRE.ABSTRACTION.ENTITIES.Request;
using MICRE.ABSTRACTION.ENTITIES.Response;
using MICRE.APPLICATION.CONNECTIONS.SERVICES;
using MICRE_APP_EXCEPCIONES.Commons;
using MICRE_APP_EXCEPCIONES.Models;
using MICRE_APP_EXCEPCIONES.SessionHandler;
using MICRE_APP_EXCEPCIONES.TDOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using NETCORE.Services.Interfaces;
using Newtonsoft.Json;
using System.Globalization;

namespace NETCORE.Controllers
{
    [Authorize]
    public class SolicitudController : Controller
    {
        private readonly SessionHandler _sessionHandler;
        private readonly IFormularioInmuebleService _service;
        private readonly IExcepcionesConnectionServices _excepcionConnectionService;
        private readonly IUserAutonomyVerification _excepcionCommons;
        private readonly IConfiguration _configuration;
        public SolicitudController(IExcepcionesConnectionServices excepcionConnectionService, IFormularioInmuebleService service, SessionHandler sessionHandler, IUserAutonomyVerification excepcionCommons, IConfiguration configuration)
        {
            _sessionHandler = sessionHandler;
            _excepcionConnectionService = excepcionConnectionService;
            _service = service ?? throw new ArgumentNullException(nameof(service));
            this._excepcionCommons = excepcionCommons;
            _configuration = configuration;
        }
        public async Task<IActionResult> SolicitudTable()
        {
            ViewBag.Message = HttpContext.Session.GetString("message");
            HttpContext.Session.Remove("message");
            var usuarioRecuperado = SessionHandler<TokenData>("token");
            await _sessionHandler.SignInAsync(usuarioRecuperado?.Matricula);
            ViewBag.NombreUsuario = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo;
            var response = await _excepcionConnectionService.ObtenerExcepciones(usuarioRecuperado?.Cargo, usuarioRecuperado?.NombreUsuario);
            return View(response.data);
        }
        public IActionResult TalbaNFilas(int n)
        {
            return View();
        }

        public async Task<IActionResult> ResumenCaso(int idCliente)
        {
            ViewBag.Message = HttpContext.Session.GetString("message");
            HttpContext.Session.Remove("message");
            var usuarioRecuperado = SessionHandler<TokenData>("token");
            await _sessionHandler.SignInAsync(usuarioRecuperado?.Matricula);
            ViewBag.NombreUsuario = usuarioRecuperado?.NombreUsuario;
            ViewBag.CargoUsuario = usuarioRecuperado?.Cargo;
            var response = await _excepcionConnectionService.ObtenerExcepcionByIdCliente(idCliente);

            ViewBag.IdClienteCaso = idCliente;
            /// formato de miles
            ViewBag.DeudaInmuebleTotal = "USD " + (response?.data?.DeudaInmueble?.TotalGarantia.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaValorInmuebleUSD = "USD " + (response?.data?.DeudaInmueble?.ValorInmuebleUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaValorInmuebleLibreDeudaUSD = "USD " + (response?.data?.DeudaInmueble?.ValorInmuebleLibreDeudaUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaBCPBOL = "BS " + (response?.data?.DeudaPersonal?.DeudaBCPBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaBCPUSD = "USD " + (response?.data?.DeudaPersonal?.DeudaBCPUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaSFNBOL = "BS " + (response?.data?.DeudaPersonal?.DeudaSFNBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaSFNUSD = "USD " + (response?.data?.DeudaPersonal?.DeudaSFNUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");

            ViewBag.DeudaBCPBOLCon = "BS " + (response?.data?.DeudaPersonalConyugue?.DeudaBCPBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaBCPUSDCon = "USD " + (response?.data?.DeudaPersonalConyugue?.DeudaBCPUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaSFNBOLCon = "BS " + (response?.data?.DeudaPersonalConyugue?.DeudaSFNBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.DeudaSFNUSDCon = "USD " + (response?.data?.DeudaPersonalConyugue?.DeudaSFNUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.CeludaConyugue = response?.data?.InformacionPersonalConyugue?.Extension.Substring(1);

            ViewBag.IngresoLiquidoBOLCon = "BS " + (response?.data?.InformacionLaboralConyugue?.IngresoLiquidoBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoBOL2Con = "BS " + (response?.data?.InformacionLaboralConyugue?.IngresoLiquidoBOL2.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoUSDCon = "USD " + (response?.data?.InformacionLaboralConyugue?.IngresoLiquidoUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoUSD2Con = "USD " + (response?.data?.InformacionLaboralConyugue?.IngresoLiquidoUSD2.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");

            ViewBag.IngresoLiquidoBOL = "BS " + (response?.data?.InformacionLaboral?.IngresoLiquidoBOL.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoBOL2 = "BS " + (response?.data?.InformacionLaboral?.IngresoLiquidoBOL2.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoUSD = "USD " + (response?.data?.InformacionLaboral?.IngresoLiquidoUSD.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.IngresoLiquidoUSD2 = "USD " + (response?.data?.InformacionLaboral?.IngresoLiquidoUSD2.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");

            ViewBag.DeudaYTotal = "USD " + (response?.data?.DeudaPersonal.MontoTotalCreditoBcp.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) ?? "0.00");
            ViewBag.HadCard = response?.data?.Productos?.Any(producto => producto.ProductoDescripcion.ToLower().Contains("tarjeta")) ?? false;
            
            ViewBag.maxSizeFile = _configuration.GetValue<int>("Configuration.Site:MaxFilesizeExcepcion");

            //response.data.Excepciones = await this._excepcionCommons.VerifyUserAutonomi(usuarioRecuperado.UserExcepcionesFiltro,response.data.Excepciones);
            return View(response.data);
        }

        [HttpPost]
        public async Task<JsonResult> AdministrarExcepcion(AdministrarExcepcionRequest _request)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            _request.Matricula = usuario.NombreUsuario + " - " + usuario.Cargo; 
            var response = await _excepcionConnectionService.AdministrarExcepcion(_request);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerExcepcionesCliente(int idClienteExcepcion)
        {
            var usuarioRecuperado = SessionHandler<TokenData>("token");
            string UsuarioRegistro = $"{usuarioRecuperado.NombreUsuario.Trim()} - {usuarioRecuperado.Cargo.Trim()}";
            var response = await _excepcionConnectionService.ObtenerExcepcionesClienteById(idClienteExcepcion, UsuarioRegistro);
            response.data = await this._excepcionCommons.VerifyUserAutonomi(usuarioRecuperado?.UserExcepcionesFiltro ?? new ExcepcionesFiltroUsuario(), response.data ?? new List<ExcepcionCliente>(), UsuarioRegistro);
            //var _maxSizeFileUpload = _configuration.GetValue<int>("Configuration.Site:MaxFilesizeExcepcion");
            ViewBag.maxSizeFile = _configuration.GetValue<int>("Configuration.Site:MaxFilesizeExcepcion");
            return Json(response.data);
        }

        [HttpPost]
        public async Task<JsonResult> ResponderExcepcionCliente(ResponderExcepcionClienteRequest _request)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            _request.Matricula = usuario.NombreUsuario + " - " + usuario.Cargo;
            var response = await _excepcionConnectionService.ResponderExcepcionCliente(_request);
            HttpContext.Session.SetString("message", response.message);
            return Json(response.data);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerRespaldoExcepcion(int _idCliente)
        {
            var response = await _excepcionConnectionService.ObtenerExcepcionesRespaldos(_idCliente);
            return Json(response.data);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerExcepciones()
        {
            var usuarioRecuperado = SessionHandler<TokenData>("token");
            var response = await _excepcionConnectionService.ObtenerExcepciones(usuarioRecuperado.Cargo, usuarioRecuperado.NombreUsuario);
            return Json(response.data);
        }

        [HttpPost]
        public async Task<JsonResult> GenerarDocumentoCliente(int idCliente)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            var _request = new ReporteDetalleRequest { idCliente = idCliente, usuario = usuario.Matricula ?? string.Empty };
            var response = await _excepcionConnectionService.GenerarDocumentoCliente(_request);
            return Json(response.reportGeneric);
        }

        [HttpPost]
        public async Task<JsonResult> DesestimarCasoCliente(int idCliente)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            var _request = new DesestimarCasoRequest { IdCliente = idCliente, Matricula = usuario.Matricula ?? string.Empty };
            var response = await _excepcionConnectionService.DesestimarCasoCliente(_request);
            HttpContext.Session.SetString("message", response.message);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarExcepcion(int idProductoExcepcion)
        {
            var responseExceptions = await _service.PostRemoveException(idProductoExcepcion.ToString());
            HttpContext.Session.SetString("message", responseExceptions.Message);
            return Json(responseExceptions);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTipoExcepciones()
        {
            var response = await _service.GetException();
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarExcepcion(UpdateExceptionRequest _request)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            _request.Matricula = usuario?.NombreUsuario + " - " + usuario?.Cargo ?? string.Empty;
            var _response = await _service.PostUpdateException(_request);
            HttpContext.Session.SetString("message", _response.Message);
            return Json(_response);
        }

        [HttpPost]
        public async Task<JsonResult> AgregarExcepcion(RegisterExceptionTDO _request)
        {
            var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
            _request.matricula = usuario?.NombreUsuario + " - " + usuario?.Cargo ?? string.Empty;
            var _response = await _service.RegistrarException(_request);
            return Json(_response);
        }
        public T? SessionHandler<T>(string session) where T : class
        {
            var miObjetoJson = HttpContext.Session.GetString(session);
            if (miObjetoJson != null)
            {
                var miObjeto = JsonConvert.DeserializeObject<T>(miObjetoJson);
                return miObjeto;
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> UploadFileExcepcion(IFormFile file, int IdCliente, int IdExcepcionAgregada) 
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var usuario = JsonConvert.DeserializeObject<TokenData>(HttpContext.Session.GetString("token") ?? string.Empty) ?? new();
                    string usuarioDetalle = usuario.NombreUsuario + " - " + usuario.Cargo;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        string fileBase64 = Convert.ToBase64String(fileBytes);
                        string fileType = file.ContentType;

                        DocumentoRespaldoExcepcionRequest _requestSend = new()
                        {
                            IdCliente = IdCliente,
                            TipoRespaldo = "RESPALDO",
                            Data = fileBase64, 
                            Matricula = usuarioDetalle,
                            NombreArchivo = file.FileName,
                            TipoArchivo = fileType, 
                            IdExcepcion = IdExcepcionAgregada
                        };

                        var _response = await _service.PostRegisterBackupExcepcion(_requestSend);
                        return Json(_response);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

    }
}

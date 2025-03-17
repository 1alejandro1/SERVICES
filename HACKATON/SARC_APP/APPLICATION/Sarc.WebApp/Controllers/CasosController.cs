using AutoMapper;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Lexico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models.Caso;
using SARCAPP.APPLICATION.Sarc.WebApp.Models.Caso;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.CROSS.COMMON.Helpers;
using System.Text.Json;
using BCP.Framework.Logs;
using BCP.CROSS.MODELS.SharePoint;

namespace Sarc.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CasosController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthService _authService;
        private readonly ILexicoService _lexicoService;
        private readonly IMapper _mapper;
        private readonly ICaseService _caseService;
        private readonly IFileService _fileService;

        public CasosController(
            ILoggerManager logger,
            IAuthService authService,
            ILexicoService lexicoService,
            IMapper mapper,
            ICaseService caseService,
            IFileService fileService
        )
        {
            _logger = logger;
            _authService = authService;
            _lexicoService = lexicoService;
            _mapper = mapper;
            _caseService = caseService;
            _fileService = fileService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ING")]
        public IActionResult ProcesoRegistro(ProcessCasoViewModel responseViewModel)
        {
            return View(responseViewModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTANA")]
        public async Task<IActionResult> Detalle(string nroCaso)
        {
            var response = await _caseService.GetCasoByNroCasoAsync(nroCaso);
            return View(response);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REG_EXP")]
        public async Task<IActionResult> RegistroCasoExpress(GetClienteResponse getClienteResponse)
        {
            if (getClienteResponse.NroIdc == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _authService.GetToken();

            CreateCasoExpressViewModel createCasoViewModel = new();
            createCasoViewModel.GetClienteResponse = getClienteResponse;
            createCasoViewModel.GetClienteResponse.NombreCompleto = !string.IsNullOrEmpty(getClienteResponse.NombreCompleto) ? getClienteResponse.NombreCompleto
               : $"{getClienteResponse.PaternoRazonSocial} {getClienteResponse.Materno} {getClienteResponse.Materno}";

            var productos = await _lexicoService.GetProductosAsync();
            createCasoViewModel.ProductoDropDown = productos.Select(x => new SelectListItem(x.Descripcion, x.IdProducto));

            var sucursales = await _lexicoService.GetSucursalesAsync();
            createCasoViewModel.AtmSucursalDropDown = sucursales.Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.Sucursal));

           

            createCasoViewModel.DepartamentoDropDown = sucursales.Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.Sucursal));
            createCasoViewModel.DepartamentoDropDown = createCasoViewModel.DepartamentoDropDown.Append(new SelectListItem("PANDO", "901"));


            var viasNotificacion = await _lexicoService.GetViasNotificacionAsync();
            createCasoViewModel.ViaEnvioCodigoDropDown = viasNotificacion.Select(x => new SelectListItem(x.Via, x.Id.ToString()));

            var tiposSolucion = await _lexicoService.GetTiposSolucionAsync();
            createCasoViewModel.TipoSolucionDropDown = tiposSolucion
                .Select(x => new SelectListItem(x.Descripcion.ToUpper(), string.IsNullOrEmpty(x.IdSolucion) ? "00" : x.IdSolucion));

            var centros = await _lexicoService.GetCentrosCostoAsync();
            createCasoViewModel.CentroDeCostoDropDown = centros.Select(x => new SelectListItem(x.Descripcion, x.EstadoPrincipal));

            createCasoViewModel.Analista = user.Matricula;
           
            return View(createCasoViewModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ING")]
        public async Task<IActionResult> RegistroCaso(GetClienteResponse getClienteResponse)
        {
            if (getClienteResponse.NroIdc == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CreateCasoViewModel createCasoViewModel = new();
            createCasoViewModel.GetClienteResponse = getClienteResponse;
            createCasoViewModel.GetClienteResponse.NombreCompleto = !string.IsNullOrEmpty(getClienteResponse.NombreCompleto) ? getClienteResponse.NombreCompleto
                : $"{getClienteResponse.PaternoRazonSocial} {getClienteResponse.Materno} {getClienteResponse.Materno}";

            var productos = await _lexicoService.GetProductosAsync();
            createCasoViewModel.ProductoDropDown = productos.Select(x => new SelectListItem(x.Descripcion, x.IdProducto));

            var sucursales = await _lexicoService.GetSucursalesAsync();
            createCasoViewModel.AtmSucursalDropDown = sucursales.Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.Sucursal));

            createCasoViewModel.DepartamentoDropDown = sucursales.Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.Sucursal));
            createCasoViewModel.DepartamentoDropDown = createCasoViewModel.DepartamentoDropDown.Append(new SelectListItem("PANDO", "901"));

            var viasNotificacion = await _lexicoService.GetViasNotificacionAsync();
            createCasoViewModel.ViaEnvioCodigoDropDown = viasNotificacion.Select(x => new SelectListItem(x.Via, x.Id.ToString()));

            return View(createCasoViewModel);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REG_EXP")]
        public async Task<IActionResult> RegistroCasoExpress(CreateCasoExpressViewModel createCasoViewModel)
        {
            var user = await _authService.GetToken();

            ProcessCasoViewModel responseViewModel = new();
            switch (createCasoViewModel.Accion)
            {
                case "D":
                    createCasoViewModel.EsDevolucion = true;
                    break;
                case "C":
                    createCasoViewModel.EsCobro = true;
                    break;
            }

            createCasoViewModel.Caso.ArchivosAdjuntos = ArchivosAdjuntos(createCasoViewModel.Files);
            createCasoViewModel.Caso.FechaTxn = createCasoViewModel.FechaTxn.ToString("yyyyMMdd");
            createCasoViewModel.Caso.HoraTxn = createCasoViewModel.FechaTxn.ToString("HH:ss");
            createCasoViewModel.Caso.TipoCarta = "00";
            createCasoViewModel.Caso.FuncionarioRegistro = user.Matricula;
            createCasoViewModel.Caso.NombreUsuario = user.Nombre;
            createCasoViewModel.Caso.NroCuenta = createCasoViewModel.Caso.NroCuenta?.Trim();
            createCasoViewModel.Caso.NroTarjeta = createCasoViewModel.Caso.NroTarjeta?.Trim();

            var request = _mapper.Map<CasoExpressRequest>(createCasoViewModel);

            _logger.Information($"Request: {JsonSerializer.Serialize(request)}");
            var response = await _caseService.AddCasoExpressAsync(request);
            _logger.Information($"Response: {JsonSerializer.Serialize(response)}");

            if (!string.IsNullOrEmpty(response?.Data?.NroCarta))
            {
                HttpContext.Session.Set<string>("nrocaso", response?.Data?.NroCarta);
                HttpContext.Session.Set<string>("pdfcaso", response?.Data?.PDF);
            }

            responseViewModel.Cliente = new GetClienteResponse
            {
                NombreCompleto = string.IsNullOrEmpty(createCasoViewModel.Caso.Empresa) ? createCasoViewModel.Caso.Nombres : createCasoViewModel.Caso.Empresa,
                Idc = $"{createCasoViewModel.Caso.ClienteIdc}  {createCasoViewModel.Caso.ClienteIdcTipo } {createCasoViewModel.Caso.ClienteIdcExtension}",
            };
            responseViewModel.Response = response;
            responseViewModel.TipoRegistro = createCasoViewModel.Caso.TipoRegistro;

            return View("ProcesoRegistro", responseViewModel);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ING")]
        public async Task<IActionResult> RegistroCaso(CreateCasoViewModel createCasoViewModel)
        {
            var user = await _authService.GetToken();

            ProcessCasoViewModel responseViewModel = new();

            createCasoViewModel.Caso.ArchivosAdjuntos = ArchivosAdjuntos(createCasoViewModel.Files);
            createCasoViewModel.Caso.FechaTxn = createCasoViewModel.FechaTxn.ToString("yyyyMMdd");
            createCasoViewModel.Caso.HoraTxn = createCasoViewModel.FechaTxn.ToString("HH:ss");
            createCasoViewModel.Caso.TipoCarta = "00";
            createCasoViewModel.Caso.FuncionarioRegistro = user.Matricula;
            createCasoViewModel.Caso.NombreUsuario = user.Nombre;
            createCasoViewModel.Caso.NroCuenta = createCasoViewModel.Caso.NroCuenta?.Trim();
            createCasoViewModel.Caso.NroTarjeta = createCasoViewModel.Caso.NroTarjeta?.Trim();
            

            var request = createCasoViewModel.Caso;

            _logger.Information($"Request: {JsonSerializer.Serialize(request)}");
            var response = await _caseService.AddCasoAsync(request);
            _logger.Information($"Response: {JsonSerializer.Serialize(response)}");

            if (!string.IsNullOrEmpty(response?.Data?.NroCarta))
            {
                HttpContext.Session.Set<string>("nrocaso", response?.Data?.NroCarta);
                HttpContext.Session.Set<string>("pdfcaso", response?.Data?.PDF);
            }

            responseViewModel.Cliente = new GetClienteResponse
            {
                NombreCompleto = string.IsNullOrEmpty(createCasoViewModel.Caso.Empresa)? createCasoViewModel.Caso.Nombres : createCasoViewModel.Caso.Empresa,
                Idc = $"{createCasoViewModel.Caso.ClienteIdc}  {createCasoViewModel.Caso.ClienteIdcTipo } {createCasoViewModel.Caso.ClienteIdcExtension}",
            };
            responseViewModel.Response = response;
            responseViewModel.TipoRegistro = createCasoViewModel.Caso.TipoRegistro;

            return View("ProcesoRegistro", responseViewModel);
        }


        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidateCuentaRepext([Bind(Prefix = "Caso.NroCuenta")] string nroCuenta, [Bind(Prefix = "Caso.ClienteIdc")] string clienteIdc,
            [Bind(Prefix = "Caso.ClienteIdcTipo")] string clienteIdcTipo, [Bind(Prefix = "Caso.ClienteIdcExtension")] string clienteIdcExtension)
        {
            _logger.Information($"Request: nroCuenta: {nroCuenta}, clienteIdc: {clienteIdc}, clienteIdcTipo: {clienteIdcTipo}, clienteIdcExtension: {clienteIdcExtension}");
            var ctaRepext = await _lexicoService.ValidateCuentaAsync(nroCuenta, clienteIdc, clienteIdcTipo, clienteIdcExtension);
            _logger.Information($"Response: {JsonSerializer.Serialize(ctaRepext)}");
            if (ctaRepext != null && ctaRepext.Any())
            {
                return Json("true");
            }

            return Json("La cuenta no pertenece al cliente");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidaMontoDevolucion([Bind(Prefix = "Devolucion.Monto")] decimal monto, [Bind(Prefix = "Devolucion.Moneda")] string moneda)
        {
            var devolucion = HttpContext.Session.Get<DevolucionProductoServicioResponse>("devolucion");

            if (string.IsNullOrEmpty(moneda))
            {
                return Json("Debe seleccionar el tipo de moneda");
            }
            if (moneda.Equals("SUS"))
            {
                var responseTC = await _lexicoService.GetTipoCambioAsync();
                var maxSUS = Math.Round(devolucion.Importe / responseTC.Data.Compra, 2);
                if (monto > maxSUS)
                {
                    return Json($"El monto debe ser menor a {maxSUS} Dolares");
                }

            }
            else if (moneda.Equals("BS") && monto > devolucion.Importe)
            {
                var maxBOL = devolucion.Importe;
                return Json($"El monto debe ser menor a {maxBOL} Bolivianos");
            }

            return Json("true");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidaLimiteDevolucionCuenta([Bind(Prefix = "Devolucion.CuentaDevolucion")] string CuentaDevolucion)
        {
            var limites = await _lexicoService.GetParametroSarcByTipoAsync("DEVOLUCION");
            var limiteCuenta = limites.ToList()[0].Descripcion;

            _logger.Information($"[Request]-> CuentaDevolucion:{CuentaDevolucion}, Lexico: CUENTA");
            var limiteDevolucion = await _lexicoService.GetLimiteDevolucionAsync(CuentaDevolucion, "CUENTA");
            _logger.Information($"[Response]-> {JsonSerializer.Serialize(limiteDevolucion)}");

            if (limiteDevolucion.Data >= int.Parse(limiteCuenta))
            {
                return Json($"Cuenta alcanzó el numero maximo de devoluciones ({limiteCuenta})");
            }

            return Json("true");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidaLimiteDevolucionAnalista([Bind(Prefix = "Analista")] string analista, [Bind(Prefix = "Accion")] string accion)
        {
            if (accion.Equals("D"))
            {
              
                var limites = await _lexicoService.GetParametroSarcByTipoAsync("DEVOLUCION");
               

                var limiteAnalista = limites.ToList()[1].Descripcion;

                _logger.Information($"[Request]-> Analista:{analista}, Lexico:{string.Empty}");
                var limiteDevolucion = await _lexicoService.GetLimiteDevolucionAsync(analista);
                _logger.Information($"[Response]-> {JsonSerializer.Serialize(limiteDevolucion)}");

                if (limiteDevolucion.Data >= int.Parse(limiteAnalista))
                {
                    return Json($"Analista alcanzó el numero maximo de devoluciones ({limiteAnalista})");
                }
            }

            return Json("true");
        }


        [HttpPost]
        public async Task<IActionResult> GetServicios([FromBody] ServicioByProductoTipoEstadoRequest request)
        {
            IEnumerable<SelectListItem> serviciosItems = new List<SelectListItem>();
            var servicios = await _lexicoService.GetServiciosAsync(request);
            if (servicios != null)
            {
                serviciosItems = servicios?.Select(x => new SelectListItem(x?.Descripcion, x?.ServicioId));
            }
            return Ok(serviciosItems);
        }

        [HttpPost]
        public async Task<IActionResult> GetCiudades([FromBody] string departamentoId)
        {
            var resp = await _lexicoService.GetCidudadesAsync(departamentoId);
            var ciudades = resp?.Select(x => new SelectListItem(x.Provincia, x.Provincia));
            return Ok(ciudades);
        }

        [HttpPost]
        public async Task<IActionResult> GetAtmsBySucrsales([FromBody] string sucursalId)
        {
            var atms = await _lexicoService.GetAtmsAsync();
            var response = atms.Where(s => s.Sucursal.Equals(sucursalId)).Select(x => new SelectListItem(x.Direccion, x.Ubicacion));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetDocumentacionRequerida([FromBody] ProductoServicioRequest request)
        {
            var response = await _lexicoService.GetDocumentacionRequerida(request);
            string documentacionRequerida = response.FirstOrDefault(d => d.IdProducto.Equals(request.ProductoId) && d.IdServicio.Equals(request.ServicioId))?.DocumentacionRequerida;
            return Ok(documentacionRequerida);
        }

        [HttpPost]
        public async Task<IActionResult> GetDevServicioCanalCuentaCon([FromBody] ServicioCanalCuentaRequest request)
        {
            var canalCuenta = new SelectListItem();
            var response = await _lexicoService.GetDevServicioCanalCuentaByProductoIdServivioIdAsync(request);
            if (response != null && response?.Count() > 0)
            {
                HttpContext.Session.Set<DevolucionProductoServicioResponse>("devolucion", response.First());
                canalCuenta = new SelectListItem(response.First().CuentaContable, response.First().ServiciosCanales.ToString(), selected: true);
            }
            return Ok(canalCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> GetCobServicioCanalCuentaCon([FromBody] ServicioCanalCuentaRequest request)
        {
            IEnumerable<DevolucionProductoServicioResponse> response = new List<DevolucionProductoServicioResponse>();
            var responeCuentaCobro = await _lexicoService.GetCobServicioCanalCuentaByProductoIdServivioIdAsync(request);
            if (responeCuentaCobro != null)
            {
                response = responeCuentaCobro;
            }
            if (request.CuentaSelected != default)
            {

                var servCanal = response.FirstOrDefault(x => x.ServiciosCanales.Equals(request.CuentaSelected));
                string monto = servCanal != null ? servCanal.Importe.ToString() : "";
                return Ok(monto);
            }
            return Ok(response);
        }

        
        public IActionResult DownloadPdf()
        {
            var name = HttpContext.Session.Get<string>("nrocaso");
            var pdfss = HttpContext.Session.Get<string>("pdfcaso");
            string strFileName = name + ".pdf";
            var pdf = Convert.FromBase64String(pdfss);

            return File(pdf, "application/pdf", strFileName);
        }

        [HttpPost("UploadSharePointAsync")]
        public async Task<IActionResult> UploadSharePointAsync(List<IFormFile> files)
        {
             var fileSp = new ArchivosAdjuntosDTO
             { 
                archivosAdjuntos = files,
                nombreCarpeta = HttpContext.Session.GetString("rutaSharepoint"),
                nombreProceso = "SOLUCION"
             };

            var response = await _fileService.UploadSharePointAsync(fileSp);
            return Ok(response);
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

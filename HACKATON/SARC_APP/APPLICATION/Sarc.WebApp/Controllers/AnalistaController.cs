using BCP.CROSS.COMMON.Constants;
using BCP.CROSS.MODELS.Caso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models.Analista;
using Sarc.WebApp.Models.Caso;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.SharePoint;
using BCP.CROSS.COMMON.Enums;
using AutoMapper;
using BCP.CROSS.MODELS.Lexico;
using Sarc.WebApp.Models.Supervisor;
using System.IO;
using BCP.Framework.Logs;
using System.Text.Json;

namespace Sarc.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnalistaController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ICaseService _caseService;
        private readonly ILexicoService _lexicoService;
        private readonly IFileService _fileService;

        public AnalistaController(
            ILoggerManager logger,
            IMapper mapper,
            IAuthService authService,
            ICaseService caseService,
            ILexicoService lexicoService,
            IFileService fileService 
            )
        {
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
            _caseService = caseService;
            _lexicoService = lexicoService;
            _fileService = fileService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTANA")]
        public async Task<IActionResult> BandejadeEntrada()
        {
            var user = await _authService.GetToken();
            var bandeja = await _caseService.GetCasosByAnalistaAsync(user.Matricula, "A");

            return View(bandeja);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTREC")]
        public async Task<IActionResult> RechazarCaso(string nroCaso)
        {
            var user = await _authService.GetToken();
            var casoResponse = await _caseService.GetCasoByNroCasoAsync(nroCaso);
            if (casoResponse?.Data != null && !casoResponse.Data.Estado.Equals("A") && !user.Matricula.Equals(casoResponse.Data.FuncionarioAtencion))
            {
                return RedirectToAction("BandejadeEntrada");
            }

            var caso = _mapper.Map<RechazarCasoViewModel>(casoResponse.Data);

            var productos = await _lexicoService.GetProductosAsync();
            caso.ProductoDropDown = productos.Select(x => new SelectListItem(x.Descripcion, x.IdProducto, x.IdProducto.Equals(caso.ProductoId)));

            var servicioRequest = new ServicioByProductoTipoEstadoRequest { ProductoId = caso.ProductoId, TipoServicio = caso.TipoServicio };

            var servicios = await _lexicoService.GetServiciosAsync(servicioRequest);
            caso.ServicioDropDown = servicios.Select(x => new SelectListItem(x.Descripcion, x.ServicioId, x.ServicioId.Equals(caso.ServicioId)));

            var errors = await _lexicoService.GetSarcErrors();
            caso.TipoErrorDropDown = errors
                .Select(x => new SelectListItem(x.ErrorDescripcion, x.ErrorId));

            caso.AntServ = casoResponse.Data.ServicioId;
            caso.Documentacion = casoResponse.Data.DocumentacionAdjuntaEntrada;
            caso.Estado = EstadoCaso.RechazoIngreso;

            return View(caso);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTREC")]
        public async Task<IActionResult> ProcesoRechazoCaso(RechazarCasoViewModel rechazarCasoViewModel)
        {
            var request = _mapper.Map<UpdateCasoRechazoAnalistaDTO>(rechazarCasoViewModel);
            ProccessRechazoCasoViewModel proccessRechazoCasoViewModel = new();

            proccessRechazoCasoViewModel.NroCaso = request.NroCaso;

            _logger.Information($"[RechazarCaso Request]-> {JsonSerializer.Serialize(request)}");
            proccessRechazoCasoViewModel.CasoRechazado = await _caseService.RechazarCasoAsignadoAsync(request);
            _logger.Information($"[RechazarCaso Response]-> {JsonSerializer.Serialize(proccessRechazoCasoViewModel.CasoRechazado)}");

            return View(proccessRechazoCasoViewModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTREC")]
        public async Task<IActionResult> SolucionCaso(string nroCaso)
        {
            SolucionCasoViewModel solucionCasoViewModel = new();
            var user = await _authService.GetToken();
            var casoResponse = await _caseService.GetCasoByNroCasoAsync(nroCaso);
            if (casoResponse?.Data != null && !casoResponse.Data.Estado.Equals("A") && !user.Matricula.Equals(casoResponse.Data.FuncionarioAtencion))
            {
                return RedirectToAction("BandejadeEntrada");
            }

            var tiposSolucion = await _lexicoService.GetTiposSolucionAsync();
            solucionCasoViewModel.TipoSolucionDropDown = tiposSolucion
                .Select(x => new SelectListItem(x.Descripcion.ToUpper(), string.IsNullOrEmpty(x.IdSolucion) ? "00" : x.IdSolucion));

            var centros = await _lexicoService.GetCentrosCostoAsync();
            solucionCasoViewModel.CentroDeCostoDropDown = centros.Select(x => new SelectListItem(x.Descripcion, x.EstadoPrincipal));

            var cartas = await _lexicoService.GetCartaAllAsync();
            solucionCasoViewModel.CartaDropDown = cartas.Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.CartaId));

            var areas = await _lexicoService.GetAreaAllAsync();
            solucionCasoViewModel.AreaDropDown = areas.Select(x => new SelectListItem(x.Descripcion, x.idArea));

            var tiempos = await _lexicoService.GetParametroSarcByTipoAsync("TIEMPO");
            solucionCasoViewModel.TiempoDropDown = tiempos.Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString()));

            var procesos = await _lexicoService.GetParametroSarcByTipoAsync("PROCESO");
            solucionCasoViewModel.ProcesoDropDown = procesos.Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString()));

            var riesgos = await _lexicoService.GetParametroSarcByTipoAsync("RIESGO");
            solucionCasoViewModel.RiesgoDropDown = riesgos.Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString()));



            solucionCasoViewModel.ViaRespuestaBtns = new List<SelectListItem>{
                new SelectListItem("En Oficina BCP", "O", casoResponse.Data.ViaEnvioRespuesta.Equals("O")),
                new SelectListItem("Email", "E", casoResponse.Data.ViaEnvioRespuesta.Equals("E")),
                new SelectListItem("WhatsApp", "W", casoResponse.Data.ViaEnvioRespuesta.Equals("W"))
            };

            solucionCasoViewModel.Caso = casoResponse.Data;
            solucionCasoViewModel.Analista = casoResponse.Data.FuncionarioAtencion;

            return View(solucionCasoViewModel);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENTREC")]
        public async Task<IActionResult> SolucionCaso(SolucionCasoViewModel solucionCasoViewModel)
        {
            var user = await _authService.GetToken();

            solucionCasoViewModel.DevCob = DevolucionCobroRequest(solucionCasoViewModel);

            solucionCasoViewModel.OrigenCaso.NroCarta = solucionCasoViewModel.UpdateCaso.NroCarta;

            solucionCasoViewModel.InfoAdicional.NroCarta = solucionCasoViewModel.UpdateCaso.NroCarta;

            solucionCasoViewModel.UpdateCaso.FuncionarioModificacion = user.Matricula;
            solucionCasoViewModel.UpdateCaso.Estado = EstadoCaso.Solucionado;
            solucionCasoViewModel.UpdateCaso.MonedaDevolucion = solucionCasoViewModel.DevCob.Moneda;
            solucionCasoViewModel.UpdateCaso.ImporteDevolucion = solucionCasoViewModel.DevCob.Monto;

            solucionCasoViewModel.UpdateRespuesta.ComSMS = "0";
            solucionCasoViewModel.UpdateRespuesta.RespuestaEnviada = "0";
            solucionCasoViewModel.UpdateRespuesta.ComTelefono = "0";
            solucionCasoViewModel.UpdateRespuesta.ComEmail = solucionCasoViewModel.UpdateRespuesta.ViaEnvio == "E" ? "1" : "0";
            solucionCasoViewModel.UpdateRespuesta.ComWhastapp = solucionCasoViewModel.UpdateRespuesta.ViaEnvio == "W" ? "1" : "0";
            solucionCasoViewModel.UpdateRespuesta.ComOficina = solucionCasoViewModel.UpdateRespuesta.ViaEnvio == "O" ? "1" : "0";

            ProcessUpdateSolucionCasoViewModel procesoActualizacion = new();
            procesoActualizacion.NroCaso = solucionCasoViewModel.UpdateCaso.NroCarta;
            procesoActualizacion.TipoCaso = solucionCasoViewModel.Caso.TipoServicio;

            _logger.Information($"[SolucionCaso Request]-> {JsonSerializer.Serialize(solucionCasoViewModel.UpdateCaso)}");
            procesoActualizacion.SolucionCaso = await _caseService.UpdateSolucionCasoAsync(solucionCasoViewModel.UpdateCaso);

            _logger.Information($"[UpdateInfoRespuesta Request]-> {JsonSerializer.Serialize(solucionCasoViewModel.UpdateRespuesta)}");
            procesoActualizacion.UpdateRespuesta = await _caseService.UpdateInfoRespuestaAsync(solucionCasoViewModel.UpdateRespuesta);

            _logger.Information($"[CasoOrigen Request]-> {JsonSerializer.Serialize(solucionCasoViewModel.OrigenCaso)}");
            procesoActualizacion.CasoOrigen = await _caseService.UpdateCasoOrigenAsync(solucionCasoViewModel.OrigenCaso);

            _logger.Information($"[InformacionAdicional Request]-> {JsonSerializer.Serialize(solucionCasoViewModel.InfoAdicional)}");
            procesoActualizacion.InformacionAdicional = await _caseService.UpdateSolucionInfoAdicionalAsync(solucionCasoViewModel.InfoAdicional);

            if (!solucionCasoViewModel.Accion.Equals("SA"))
            {
                _logger.Information($"[UpdateDevolucionCobro Request]-> {JsonSerializer.Serialize(solucionCasoViewModel.DevCob)}");
                procesoActualizacion.UpdateDevolucionCobro = await _caseService.UpdateDevolucionCobroAsync(solucionCasoViewModel.DevCob);
            }

            if (procesoActualizacion.SolucionCaso != null && procesoActualizacion.SolucionCaso?.Meta?.StatusCode == 200)
            {
                var filesSharePoint = new ArchivosAdjuntosDTO
                {
                    archivosAdjuntos = solucionCasoViewModel.Files,
                    nombreCarpeta = solucionCasoViewModel.Caso.RutaSharepoint,
                    nombreProceso = "SOLUCION"
                };
                _logger.Information($"[UploadFilesSharepoint Request]-> {JsonSerializer.Serialize(filesSharePoint)}");
                procesoActualizacion.UploadFilesSharepoint = await _fileService.UploadSharePointAsync(filesSharePoint);
            }
            _logger.Information($"[ProcesoSolucionCaso Response]-> {JsonSerializer.Serialize(procesoActualizacion)}");
            return View("ProcesoSolucionCaso", procesoActualizacion);
        }

        public IActionResult ProcesoSolucionCaso (ProcessUpdateSolucionCasoViewModel procesoActualizacion)
        {
            return View(procesoActualizacion);
        }

        private static UpdateDevolucionCobroRequest DevolucionCobroRequest(SolucionCasoViewModel solucionCasoViewModel)
        {
            var devolucionCobroRequest = new UpdateDevolucionCobroRequest { 
                    NroCarta = solucionCasoViewModel.UpdateCaso.NroCarta
                };
            
            if (solucionCasoViewModel.Accion.Equals("C"))
            {
                devolucionCobroRequest.IndDevolucionCobro = (int)Transaccion.Cobro;
                devolucionCobroRequest.Monto = decimal.Parse(solucionCasoViewModel.Cobro.Monto);
                devolucionCobroRequest.Moneda = solucionCasoViewModel.Cobro.Moneda;
                devolucionCobroRequest.TipoFacturacion = solucionCasoViewModel.Cobro.TipoFacturacion;
                devolucionCobroRequest.IdServiciosCanales = solucionCasoViewModel.Cobro.ServiciosCanalesId;
                devolucionCobroRequest.NroCuentaPR = solucionCasoViewModel.Cobro.CuentaCobro;
            }
            else if (solucionCasoViewModel.Accion.Equals("D"))
            {
                devolucionCobroRequest.IndDevolucionCobro = (int)Transaccion.Devolucion;
                devolucionCobroRequest.Monto = decimal.Parse(solucionCasoViewModel.Devolucion.Monto);
                devolucionCobroRequest.Moneda = solucionCasoViewModel.Devolucion.Moneda;
                devolucionCobroRequest.ParametroCentroPR = solucionCasoViewModel.Devolucion.ParametroCentro;
                devolucionCobroRequest.IdServiciosCanales = solucionCasoViewModel.Devolucion.ServiciosCanalesId;
                devolucionCobroRequest.NroCuentaPR = solucionCasoViewModel.Devolucion.CuentaDevolucion;
            }

            return devolucionCobroRequest;
        }
        
        
    }
}

using BCP.CROSS.COMMON.Constants;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.Lexico;
using BCP.CROSS.MODELS.SharePoint;
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
using BCP.CROSS.MODELS.WcfSwamp;
using BCP.CROSS.MODELS;
using AutoMapper;
using BCP.CROSS.COMMON.Enums;
using Sarc.WebApp.Models.Supervisor;
using System;
using System.Drawing;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using BCP.CROSS.MODELS.Carta;
using BCP.Framework.Logs;
using System.Text.Json;

namespace Sarc.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SupervisorController : Controller
    {
        private readonly IReportesService _reportesService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ICaseService _caseService;
        private readonly ILexicoService _lexicoService;
        private readonly IFileService _fileService;
        private readonly IWcfSwampService _wcfSwampService;

        public SupervisorController(
            ILoggerManager logger,
            IMapper mapper,
            IAuthService authService,
            ICaseService caseService,
            ILexicoService lexicoService,
            IFileService fileService,
            IWcfSwampService wcfSwampService,
            IReportesService reportesService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
            _caseService = caseService;
            _lexicoService = lexicoService;
            _fileService = fileService;
            _wcfSwampService = wcfSwampService;
            _reportesService = reportesService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_BANENT")]
        public async Task<IActionResult> BandejadeEntrada()
        {
            var bandeja = await _caseService.GetCasosByEstadoAsync("S");

            return View(bandeja);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_SOLCAS")]
        public async Task<IActionResult> ProcesoRechazoCaso(SolucionCasoSupervisorViewModel solucionCasoSupervisorViewModel)
        {
            solucionCasoSupervisorViewModel.RechazarCaso.FechaProrroga = solucionCasoSupervisorViewModel.RechazarCaso.Fecha != default ?
            solucionCasoSupervisorViewModel.RechazarCaso.Fecha.ToString("yyyMMdd") : string.Empty;

            solucionCasoSupervisorViewModel.RechazarCaso.SW = "S";
            ProccessRechazoCasoViewModel proccessRechazoCasoViewModel = new();

            proccessRechazoCasoViewModel.NroCaso = solucionCasoSupervisorViewModel.RechazarCaso.NroCaso;

            _logger.Information($"[RechazarCaso Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.RechazarCaso)}");
            proccessRechazoCasoViewModel.CasoRechazado = await _caseService.RechazarCasoSolucionadoAsync(solucionCasoSupervisorViewModel.RechazarCaso);
            _logger.Information($"[RechazarCaso Response]-> {JsonSerializer.Serialize(proccessRechazoCasoViewModel.CasoRechazado)}");

            return View(proccessRechazoCasoViewModel);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_SOLCAS")]
        public async Task<IActionResult> SolucionCaso(string nroCaso, string matricula, string funcionario)
        {
            SolucionCasoSupervisorViewModel solucionCasoViewModel = new();

            var casoAll = await _caseService.GetCasoAllAsync(nroCaso);
            if (casoAll?.Data != null && !casoAll.Data.Estado.Equals("S"))
            {
                return RedirectToAction("BandejadeEntrada");
            }

            var casoResponse = await _caseService.GetCasoByNroCasoAsync(nroCaso);
            var tiposSolucion = await _lexicoService.GetTiposSolucionAsync();
            solucionCasoViewModel.TipoSolucionDropDown = tiposSolucion
                .Select(x => new SelectListItem(x.Descripcion.ToUpper(), string.IsNullOrEmpty(x.IdSolucion) ? "00" : x.IdSolucion, x.IdSolucion.Equals(casoAll.Data.TipoSolucion)));

            var centros = await _lexicoService.GetCentrosCostoAsync();
            solucionCasoViewModel.CentroDeCostoDropDown = centros
                .Select(x => new SelectListItem(x.Descripcion, x.EstadoPrincipal, x.Descripcion.Equals(casoAll.Data.CentroCostoPR)));

            var cartas = await _lexicoService.GetCartaAllAsync();
            solucionCasoViewModel.CartaDropDown = cartas
                .Select(x => new SelectListItem(x.Descripcion.ToUpper(), x.CartaId, x.CartaId.Equals(casoAll.Data.TipoCarta)));

            var errors = await _lexicoService.GetSarcErrors();
            solucionCasoViewModel.TipoErrorDropDown = errors
                .Select(x => new SelectListItem(x.ErrorDescripcion, x.ErrorId));

            var areas = await _lexicoService.GetAreaAllAsync();
            solucionCasoViewModel.AreaDropDown = areas.Select(x => new SelectListItem(x.Descripcion, x.idArea, x.idArea.Equals(casoAll.Data.AreaOrigen)));

            var tiempos = await _lexicoService.GetParametroSarcByTipoAsync("TIEMPO");
            solucionCasoViewModel.TiempoDropDown = tiempos
                .Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString(), x.IdParametro.Equals(casoAll.Data.ParametroProceso)));

            var procesos = await _lexicoService.GetParametroSarcByTipoAsync("PROCESO");
            solucionCasoViewModel.ProcesoDropDown = procesos
                .Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString(), x.IdParametro.Equals(casoAll.Data.ParametroTiempo)));

            var riesgos = await _lexicoService.GetParametroSarcByTipoAsync("RIESGO");
            solucionCasoViewModel.RiesgoDropDown = riesgos
                .Select(x => new SelectListItem(x.Descripcion, x.IdParametro.ToString(), x.IdParametro.Equals(casoAll.Data.ParametroRiesgo)));

            solucionCasoViewModel.ViaRespuestaBtns = new List<SelectListItem>{
                new SelectListItem("En Oficina BCP", "O", casoResponse.Data.ViaEnvioRespuesta.Equals("O")),
                new SelectListItem("Email", "E", casoResponse.Data.ViaEnvioRespuesta.Equals("E")),
                new SelectListItem("WhatsApp", "W", casoResponse.Data.ViaEnvioRespuesta.Equals("W"))
            };

            solucionCasoViewModel.Accion = casoAll.Data.DevCredPR switch
            {
                1 => "C",
                2 => "D",
                _ => "SA"
            };

            #region Poblar Cobro
            solucionCasoViewModel.Cobro = new();
            solucionCasoViewModel.Cobro.TipoFacturacion = casoAll.Data.TipoFacturacionPR;
            solucionCasoViewModel.Cobro.Monto = casoAll.Data.MontoPR.ToString();
            solucionCasoViewModel.Cobro.Moneda = casoAll.Data.MonedaPR;
            solucionCasoViewModel.Cobro.CuentaCobro = casoAll.Data.CuentaPR;
            solucionCasoViewModel.Cobro.DescripcionServicio = casoResponse.Data.Servicio;
            solucionCasoViewModel.Cobro.ServiciosCanalesId = casoAll.Data.IdServiciosCanalesPR;
            #endregion

            #region Poblar Devolucion
            solucionCasoViewModel.Devolucion = new();
            solucionCasoViewModel.Devolucion.Monto = casoAll.Data.MontoPR.ToString();
            solucionCasoViewModel.Devolucion.Moneda = casoAll.Data.MonedaPR;
            solucionCasoViewModel.Devolucion.CuentaDevolucion = casoAll.Data.CuentaPR;
            solucionCasoViewModel.Devolucion.DescripcionServicio = casoResponse.Data.Servicio;
            solucionCasoViewModel.Devolucion.ServiciosCanalesId = casoAll.Data.IdServiciosCanalesPR;
            solucionCasoViewModel.Devolucion.ParametroCentro = casoAll.Data.CentroCostoPR;
            #endregion

            #region Poblar UpdatedSolucion
            solucionCasoViewModel.UpdateCaso = new();
            solucionCasoViewModel.UpdateCaso.DescripcionSolucion = casoAll.Data.DescripcionSolucion;
            solucionCasoViewModel.UpdateCaso.DocumentoAdjuntoOut = casoAll.Data.DocumentacionAdjuntaSalida;
            solucionCasoViewModel.UpdateCaso.TipoSolucion = casoAll.Data.TipoSolucion;
            solucionCasoViewModel.UpdateCaso.TipoCarta = casoAll.Data.TipoCarta;
            #endregion

            solucionCasoViewModel.CuentaContableDropDown = await CuentaCobDev(casoAll.Data.ProductoId, casoAll.Data.ServicioId, casoAll.Data.DevCredPR,
                                                                    casoAll.Data.IdServiciosCanalesPR);

            var user = await _authService.GetToken();
            solucionCasoViewModel.Caso = casoResponse.Data;
            solucionCasoViewModel.FuncionarioAtencion = $"{matricula} - {funcionario}";

            return View(solucionCasoViewModel);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_SOLCAS")]
        public async Task<IActionResult> SolucionCaso(SolucionCasoSupervisorViewModel solucionCasoSupervisorViewModel)
        {
            var user = await _authService.GetToken();

            solucionCasoSupervisorViewModel.DevCob = DevolucionCobroRequest(solucionCasoSupervisorViewModel);

            solucionCasoSupervisorViewModel.OrigenCaso.NroCarta = solucionCasoSupervisorViewModel.UpdateCaso.NroCarta;

            solucionCasoSupervisorViewModel.InfoAdicional.NroCarta = solucionCasoSupervisorViewModel.UpdateCaso.NroCarta;

            solucionCasoSupervisorViewModel.UpdateCaso.FuncionarioModificacion = user.Matricula;
            solucionCasoSupervisorViewModel.UpdateCaso.Estado = EstadoCaso.Solucionado;
            solucionCasoSupervisorViewModel.UpdateCaso.MonedaDevolucion = solucionCasoSupervisorViewModel.DevCob.Moneda;
            solucionCasoSupervisorViewModel.UpdateCaso.ImporteDevolucion = solucionCasoSupervisorViewModel.DevCob.Monto;
            solucionCasoSupervisorViewModel.CerrarCaso.FuncionarioSupervisor = user.Matricula;
            solucionCasoSupervisorViewModel.CerrarCaso.Documento = solucionCasoSupervisorViewModel.Caso.DocumentacionAdjuntaEntrada ?? string.Empty;

            solucionCasoSupervisorViewModel.UpdateRespuesta.ComSMS = "0";
            solucionCasoSupervisorViewModel.UpdateRespuesta.RespuestaEnviada = "0";
            solucionCasoSupervisorViewModel.UpdateRespuesta.ComTelefono = "0";
            solucionCasoSupervisorViewModel.UpdateRespuesta.ComEmail = solucionCasoSupervisorViewModel.UpdateRespuesta.ViaEnvio == "E" ? "1" : "0";
            solucionCasoSupervisorViewModel.UpdateRespuesta.ComWhastapp = solucionCasoSupervisorViewModel.UpdateRespuesta.ViaEnvio == "W" ? "1" : "0";
            solucionCasoSupervisorViewModel.UpdateRespuesta.ComOficina = solucionCasoSupervisorViewModel.UpdateRespuesta.ViaEnvio == "O" ? "1" : "0";

            ProcessUpdateSolucionCasoSupervisorViewModel procesoCierre = new();
            procesoCierre.NroCaso = solucionCasoSupervisorViewModel.UpdateCaso.NroCarta;
            procesoCierre.TipoCaso = solucionCasoSupervisorViewModel.Caso.TipoServicio;
            procesoCierre.Accion = solucionCasoSupervisorViewModel.Accion == "D" ? "Devolucion" : solucionCasoSupervisorViewModel.Accion == "C" ? "Cobro" : "";

            _logger.Information($"[UpdateSolucionCaso Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.UpdateCaso)}");
            procesoCierre.SolucionCaso = await _caseService.UpdateSolucionCasoAsync(solucionCasoSupervisorViewModel.UpdateCaso);

            _logger.Information($"[UpdateInfoRespuesta Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.UpdateRespuesta)}");
            procesoCierre.UpdateRespuesta = await _caseService.UpdateInfoRespuestaAsync(solucionCasoSupervisorViewModel.UpdateRespuesta);

            _logger.Information($"[CasoOrigen Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.OrigenCaso)}");
            procesoCierre.CasoOrigen = await _caseService.UpdateCasoOrigenAsync(solucionCasoSupervisorViewModel.OrigenCaso);

            _logger.Information($"[InformacionAdicional Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.InfoAdicional)}");
            procesoCierre.InformacionAdicional = await _caseService.UpdateSolucionInfoAdicionalAsync(solucionCasoSupervisorViewModel.InfoAdicional);

            if (!solucionCasoSupervisorViewModel.Accion.Equals("SA"))
            {
                TransactionRequest transactionRequest = await TransactionRequest(solucionCasoSupervisorViewModel);

                _logger.Information($"[UpdateDevolucionCobro Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.DevCob)}");
                procesoCierre.UpdateDevolucionCobro = await _caseService.UpdateDevolucionCobroAsync(solucionCasoSupervisorViewModel.DevCob);

                if (solucionCasoSupervisorViewModel.Accion.Equals("C"))
                {
                    var cobroRequest = _mapper.Map<CobroPR>(transactionRequest);

                    _logger.Information($"[Debito Request]-> {JsonSerializer.Serialize(cobroRequest)}");
                    procesoCierre.Transaction = await _wcfSwampService.DebitarAsync(cobroRequest);
                }

                if (solucionCasoSupervisorViewModel.Accion.Equals("D"))
                {
                    var devolucionRequest = _mapper.Map<DevolucionPR>(transactionRequest);
                    _logger.Information($"[Abono Request]-> {JsonSerializer.Serialize(devolucionRequest)}");
                    procesoCierre.Transaction = await _wcfSwampService.AbanarAsync(devolucionRequest);
                }
            }
            if ((solucionCasoSupervisorViewModel.Accion.Equals("SA")) || (procesoCierre.Transaction != null && procesoCierre.Transaction.Meta.StatusCode == 200))
            {
                _logger.Information($"[CierreCaso Request]-> {JsonSerializer.Serialize(solucionCasoSupervisorViewModel.CerrarCaso)}");
                procesoCierre.CierreCaso = await _caseService.CerrarCasoAsync(solucionCasoSupervisorViewModel.CerrarCaso);
            }

            if (procesoCierre.CierreCaso?.Meta?.StatusCode == 200)
            {
                var filesSharePoint = new ArchivosAdjuntosDTO
                {
                    archivosAdjuntos = solucionCasoSupervisorViewModel.Files,
                    nombreCarpeta = solucionCasoSupervisorViewModel.Caso.RutaSharepoint,
                    nombreProceso = "RESPUESTA"
                };

                _logger.Information($"[SubirAdjuntos Request]-> {JsonSerializer.Serialize(filesSharePoint)}");
                procesoCierre.UploadFilesSharepoint = await _fileService.UploadSharePointAsync(filesSharePoint);
            }

            _logger.Information($"[SolucionCaso Response]-> {JsonSerializer.Serialize(procesoCierre)}");
            return View("ProcesoCierreCaso", procesoCierre);
        }

        public IActionResult ProcesoCierreCaso(ProcessUpdateSolucionCasoSupervisorViewModel procesoCierre)
        {
            return View(procesoCierre);
        }

        private static UpdateDevolucionCobroRequest DevolucionCobroRequest(SolucionCasoSupervisorViewModel solucionCasoSupervisorViewModel)
        {
            var devolucionCobroRequest = new UpdateDevolucionCobroRequest
            {
                NroCarta = solucionCasoSupervisorViewModel.UpdateCaso.NroCarta
            };

            if (solucionCasoSupervisorViewModel.Accion.Equals("C"))
            {
                devolucionCobroRequest.IndDevolucionCobro = (int)Transaccion.Cobro;
                devolucionCobroRequest.Monto = decimal.Parse(solucionCasoSupervisorViewModel.Cobro.Monto);
                devolucionCobroRequest.Moneda = solucionCasoSupervisorViewModel.Cobro.Moneda;
                devolucionCobroRequest.TipoFacturacion = solucionCasoSupervisorViewModel.Cobro.TipoFacturacion;
                devolucionCobroRequest.IdServiciosCanales = solucionCasoSupervisorViewModel.Cobro.ServiciosCanalesId;
                devolucionCobroRequest.NroCuentaPR = solucionCasoSupervisorViewModel.Cobro.CuentaCobro;
            }
            else if (solucionCasoSupervisorViewModel.Accion.Equals("D"))
            {
                devolucionCobroRequest.IndDevolucionCobro = (int)Transaccion.Devolucion;
                devolucionCobroRequest.Monto = decimal.Parse(solucionCasoSupervisorViewModel.Devolucion.Monto);
                devolucionCobroRequest.Moneda = solucionCasoSupervisorViewModel.Devolucion.Moneda;
                devolucionCobroRequest.ParametroCentroPR = solucionCasoSupervisorViewModel.Devolucion.ParametroCentro;
                devolucionCobroRequest.IdServiciosCanales = solucionCasoSupervisorViewModel.Devolucion.ServiciosCanalesId;
                devolucionCobroRequest.NroCuentaPR = solucionCasoSupervisorViewModel.Devolucion.CuentaDevolucion;
            }

            return devolucionCobroRequest;
        }

        private async Task<IEnumerable<SelectListItem>> CuentaCobDev(string productoId, string servicioId, int accion, int srvCanal)
        {
            var request = new ServicioCanalCuentaRequest { IdProducto = productoId, IdServicio = servicioId };
            IEnumerable<DevolucionProductoServicioResponse> cuentaContable = accion switch
            {
                (int)Transaccion.Cobro => await _lexicoService.GetCobServicioCanalCuentaByProductoIdServivioIdAsync(request),
                (int)Transaccion.Devolucion => await _lexicoService.GetDevServicioCanalCuentaByProductoIdServivioIdAsync(request),
                (int)Transaccion.SinAccion => null,
                _ => throw new System.Exception("Cuenta Contable no disponible")
            };
            if (accion == (int)Transaccion.Devolucion)
                HttpContext.Session.Set<DevolucionProductoServicioResponse>("devolucion", cuentaContable.First());


            var response = cuentaContable?.Select(x => new SelectListItem(x.CuentaContable, x.ServiciosCanales.ToString(), x.ServiciosCanales.Equals(srvCanal)));
            return response;
        }

        private async Task<TransactionRequest> TransactionRequest(SolucionCasoSupervisorViewModel solucionCasoSupervisorViewModel)
        {
            var user = await _authService.GetToken();

            var transactionRequest = new TransactionRequest
            {
                ClienteIdc = solucionCasoSupervisorViewModel.Caso.ClienteIdc,
                ClienteIdcExtension = solucionCasoSupervisorViewModel.Caso.ClienteExtension,
                ClienteIdcTipo = solucionCasoSupervisorViewModel.Caso.ClienteTipo,
                DireccionRespuesta = solucionCasoSupervisorViewModel.UpdateRespuesta.Direccion,
                EmailRespuesta = solucionCasoSupervisorViewModel.UpdateRespuesta.Email,
                TelefonoRespuesta = solucionCasoSupervisorViewModel.UpdateRespuesta.Telefono,
                RutaSharePoint = solucionCasoSupervisorViewModel.Caso.RutaSharepoint,
                Paterno = solucionCasoSupervisorViewModel.Caso.ApellidoPaterno,
                Empresa = solucionCasoSupervisorViewModel.Caso.Empresa,
                FuncionarioAtencion = solucionCasoSupervisorViewModel.Caso.FuncionarioAtencion,
                Supervisor = user.Matricula,
                NroCaso = solucionCasoSupervisorViewModel.UpdateCaso.NroCarta,
                ProductoId = solucionCasoSupervisorViewModel.CerrarCaso.Producto,
                ServicioId = solucionCasoSupervisorViewModel.CerrarCaso.Servicio,

            };

            if (solucionCasoSupervisorViewModel.Accion.Equals("C"))
            {
                transactionRequest.DescripcionServicio = solucionCasoSupervisorViewModel.Cobro.DescripcionServicio;
                transactionRequest.Moneda = solucionCasoSupervisorViewModel.Cobro.Moneda;
                transactionRequest.Monto = decimal.Parse(solucionCasoSupervisorViewModel.Cobro.Monto);
                transactionRequest.ServiciosCanalesId = solucionCasoSupervisorViewModel.Cobro.ServiciosCanalesId;
                transactionRequest.FacturacionOnline = solucionCasoSupervisorViewModel.Cobro.TipoFacturacion == "O";
                transactionRequest.NroCuenta = solucionCasoSupervisorViewModel.Cobro.CuentaCobro;
            }

            if (solucionCasoSupervisorViewModel.Accion.Equals("D"))
            {
                transactionRequest.DescripcionServicio = solucionCasoSupervisorViewModel.Devolucion.DescripcionServicio;
                transactionRequest.Moneda = solucionCasoSupervisorViewModel.Devolucion.Moneda;
                transactionRequest.Monto = decimal.Parse(solucionCasoSupervisorViewModel.Devolucion.Monto);
                transactionRequest.ServiciosCanalesId = solucionCasoSupervisorViewModel.Devolucion.ServiciosCanalesId;
                transactionRequest.ParametroCentro = solucionCasoSupervisorViewModel.Devolucion.ParametroCentro;
                transactionRequest.NroCuenta = solucionCasoSupervisorViewModel.Devolucion.CuentaDevolucion;
            }

            return transactionRequest;
        }

        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> GetErroresRechazo()
        {
            var errores = await Task.Run(() =>
                new List<SelectListItem>{
                new SelectListItem("RESPALDOS", "RESPALDOS"),
                new SelectListItem("ORTOGRAFICO", "ORTOGRAFICO"),
                new SelectListItem("CONTENIDO", "CONTENIDO"),
                new SelectListItem("PRORROGA", "PRORROGA"),
                new SelectListItem("OTROS", "OTROS")
                }
            );

            return errores;
        }

        public async Task<FileResult> GetCartaFile(string tipo, string nroCaso)
        {

            CartaFileRequest request = new()
            {
                NroCarta = nroCaso,
                TipoCarta = tipo
            };
            ServiceResponse<string> getCartaFile = await _reportesService.GetCartaFile(request);
            string data = getCartaFile.Data;
            var stream = new MemoryStream();
            using (WordprocessingDocument document = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainpart = document.AddMainDocumentPart();
                mainpart.Document = new Document(
                    new Body(
                        new Paragraph(
                            new Run(
                            new Text(data)
                            )
                            )
                        )
                    );
            }
            stream.Position = 0;
            return File(stream, "application/vnd.ms-word.document", "Carta-"+nroCaso+".docx");
        }
    }
}

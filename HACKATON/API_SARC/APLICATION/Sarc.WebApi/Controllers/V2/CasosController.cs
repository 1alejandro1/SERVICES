using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;

namespace Sarc.WebApi.Controllers.V2
{

    [ApiVersion("2.0")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CasosController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICasoService _casoService;
        private JsonSerializerOptions options;
        private readonly IConverter _converter;

        public CasosController(ILoggerManager logger, ICasoService casoService, IConverter converter)
        {
            _logger = logger;
            _casoService = casoService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            _converter = converter;
        }

        [HttpPost("ObtenerCaso")]
        public async Task<IActionResult> GetCasoAll([FromBody] GetCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Consulta de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ObtenerCasoAll")]
        public async Task<IActionResult> GetCasoCompleto([FromBody] GetCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Consulta de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoCompletoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasosByAnalista")]
        public async Task<IActionResult> GetCasoAllByAnalista([FromBody] GetCasoDTOByAnalistaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Consulta de Caso por Analista, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoByAnalistaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasosByEstado")]
        public async Task<IActionResult> GetCasoAllByEstado([FromBody] GetCasoDTOByEstadoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Caso por Estado, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoByEstadoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasoDatosAlerta")]
        public async Task<IActionResult> ObtenerCasoDatosAlerta([FromBody] GetCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetListCasoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasoResumenByEstado")]
        public async Task<IActionResult> GetCasoResumenByEstado([FromBody] EstadoCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Casos por Estado, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoResumenByEstadoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasoByTipoAnalista")]
        public async Task<IActionResult> GetReclamosTipoAnalista([FromBody] AlertaAnalistaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Alerta Analista");
            var response = await _casoService.GetReclamoFuncionarioTipoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasoResumenByAnalista")]
        public async Task<IActionResult> GetAlertaAnalista([FromBody] MatriculaFuncionario request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Alerta Analista");
            var response = await _casoService.GetReclamoFuncionarioTipoAlertaAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasosAtencionByEstado")]
        public async Task<IActionResult> GetCasoAtencionByEstado([FromBody] EstadoCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Casos de atencion por estado, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoAtencionByEstadoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        //
        [HttpPost("ObtenerCasosAtencionByEstadoFuncionario")]
        public async Task<IActionResult> GetCasoAtencionByEstadoFuncionario([FromBody] EstadoCasoFuncionarioRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Casos de atencion por estado, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoAtencionByEstadoUsuarioAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("RegistrarCaso")]
        public async Task<IActionResult> RegistrarCaso([FromBody] CreateCasoDTOV2 request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Registro de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.AddCasoAsyncV2(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            #region Generacion del PDF
            response.Data.PDF = GenerarPdf(response.Data.PDF);
            #endregion

            return Ok(response);
        }
        //
        [HttpPost("ActualizarAsignacionCaso")]
        public async Task<IActionResult> UpdateAsignacionCasoComplejidad([FromBody] CasoComplejidad request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Modificar Casos por Estado, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateCasoEstadoComplejidadAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ActualizarCasoSolucion")]
        public async Task<IActionResult> UpdateCasoSolucion([FromBody] UpdateCasoSolucionDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Actualizacion de caso solucion, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateSolucionCasoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ActualizarCasoViaEnvio")]
        public async Task<IActionResult> UpdateCasoViaEnvio([FromBody] UpdateCasoDTOViaEnvio request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Actualizacion de caso via envio, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateViaEnvioAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ActualizarCasoSolucionFecha")]
        public async Task<IActionResult> UpdateCasoSolucionDate([FromBody] UpdateCasoSolucionDateDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Actualizacion de caso solucion, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateSolucionCasoDateAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }        

        [HttpPost("ActualizarCasoDevolucionCobro")]
        public async Task<IActionResult> UpdateDevolucionCobro([FromBody] UpdateDevolucionCobroRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Modificar Devolucion Cobro de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateDevolucionCobroAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }


            return Ok(response);
        }

        [HttpPost("ActualizarCasoOrigen")]
        public async Task<IActionResult> UpdateSolucionCasoOrigen([FromBody] UpdateSolucionCasoDTOOrigenRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Modificar Solucion Origen de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateSolucionCasoOrigenAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }


            return Ok(response);
        }       

        [HttpPost("ActualizarCasoArea")]
        public async Task<IActionResult> UpdateOrigenCaso([FromBody] UpdateOrigenCasoDTORequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Modificar Origen de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateCasoGrabarOrigenAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }


            return Ok(response);
        }       

        [HttpPost("CerrarCaso")]
        public async Task<IActionResult> CerrarCaso([FromBody] CerrarCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Cierre de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.CerrarCasoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }


            return Ok(response);
        }

        [HttpPost("RechazarCasoAsignado")]
        public async Task<IActionResult> UpdateCasoRechazoAnalista([FromBody] UpdateCasoRechazoAnalistaDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Registrar rechazo del caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateCasoRechazoAnalistaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("RechazarCasoSolucionado")]
        public async Task<IActionResult> UpdateCasoRechazar([FromBody] UpdateCasoRechazarDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Rechazar Casos, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateCasoRechazarAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ReasignarCaso")]
        public async Task<IActionResult> UpdateCasoReasignar([FromBody] UpdateCasoDTOEstado request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Reagsignar Casos, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateCasoReaperturaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("GetReporteRegistro")]
        public async Task<IActionResult> GetReporteRegistro([FromBody] DatosReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Generar Data PDF: Registro Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetReporteRegistroAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            #region Generacion del PDF
            response.Data = GenerarPdf(response.Data);
            #endregion
            return Ok(response);
        }    

        [HttpPost("CierreCasoExpress")]
        public async Task<IActionResult> CierreCasoExpress([FromBody] FlujoCasoExpressDTORequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Consulta de Caso por Analista, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.CierraCasoExpress(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Data != null && !string.IsNullOrEmpty(response.Data.PDF))
            {
                #region Generacion del PDF
                response.Data.PDF = GenerarPdf(response.Data.PDF);
                #endregion
            }
            else if (response.Meta.StatusCode == 200)
            {
                response.Data.PDF = "-COMPROBANTE GENERADO CON ANTERIORIDAD-";
            }
            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }

        private string GenerarPdf(string html)
        {
            #region Generacion del PDF
            byte[] file = { 0, 100, 120, 210, 255 };
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                Margins = new MarginSettings { Top = 7, Left = 22, Right = 15 },
                DocumentTitle = "Reporte"
            };
            var objectSettigns = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,                
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "bootstrap.min.css"), LoadImages = true },
                //FooterSettings = { FontName = "Arial", FontSize = 9, Right = "Página [page] de [toPage]", Line = true },
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettigns }
            };

            file = _converter.Convert(pdf);
            return Convert.ToBase64String(file);
            #endregion
        }

        [HttpPost("GetCasoLogsByCaso")]
        public async Task<IActionResult> GetCasoLogsByCaso([FromBody] GetCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Logs de Casos por caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoLogsAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

    }
}

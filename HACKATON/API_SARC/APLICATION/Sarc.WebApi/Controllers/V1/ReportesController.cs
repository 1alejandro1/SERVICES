using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Reportes;
using BCP.CROSS.MODELS.Reportes.Requests;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace Sarc.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IReportesService _reportesService;
        private JsonSerializerOptions options;
        public ReportesController(ILoggerManager logger, IReportesService reportesService)
        {
            _logger = logger;
            _reportesService = reportesService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }

        [HttpPost("ASFI/Registrados")]
        public async Task<IActionResult> ASFIRegistrados([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando Reporte ASFI_Registros, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.ASFIRegistradosService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ASFI/Solucionados")]
        public async Task<IActionResult> ASFISolucionados([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.ASFISolucionadosService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("Analista/CantidadCasos")]
        public async Task<IActionResult> AnalistaCantidadCasos([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.AnalistaCantidadCasoService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("Analista/CasosSolucionados")]
        public async Task<IActionResult> AnalistaCasosSolucionados([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.AnalistaCasosSolucionadosService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }


        [HttpPost("Analista/Especialidad")]
        public async Task<IActionResult> AnalistaEspecialidad([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.AnalistaEspecialidadService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("ReporteBase")]
        public async Task<IActionResult> ReporteBase([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.ReporteBaseService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("CapacidadEspecialidad")]
        public async Task<IActionResult> CapacidadEspecialidad([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _reportesService.CapacidadEspecialidadService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("CNR/Total")]
        public async Task<IActionResult> CNRTotal([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.CRNTotalService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("CNR/Detalle")]
        public async Task<IActionResult> CNRDetalle([FromBody] CRNDetalleRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.CRNDetalleService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("TipoReclamo")]
        public async Task<IActionResult> TipoReclamo([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.TipoReclamoService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("ReposicionTarjeta")]
        public async Task<IActionResult> ReposicionTarjeta([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.ReposicionTarjetaService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("DevolucionATMPOS")]
        public async Task<IActionResult> DevolucionATMPOS([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.DevolucionATMPOSService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("Expedicion")]
        public async Task<IActionResult> Expedicion([FromBody] ReporteRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.ExpedicionService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("CobrosDevoluciones")]
        public async Task<IActionResult> CobrosDevoluciones([FromBody] CobrosDevolucionesRequestModel request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);
            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");

            var response = await _reportesService.CobrosDevolucionesService(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
      
    }
}

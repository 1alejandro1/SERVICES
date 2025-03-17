using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Historico;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;

namespace Sarc.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HistoricoController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IHistoricoService _casoService;
        private JsonSerializerOptions options;

        public HistoricoController(ILoggerManager logger, IHistoricoService casoService)
        {
            _logger = logger;
            _casoService = casoService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }
        
        [HttpPost("GetCasoHistoricoByIdc")]
        public async Task<IActionResult> GetCasoHistoricoByIdc([FromBody] ClienteIdcRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Casos Historicos por IDC, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoHistoricoByIdcAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        
        [HttpPost("GetCasoHistoricoByIdcFecha")]
        public async Task<IActionResult> GetCasoHistoricoByIdcFecha([FromBody] ClienteIdcFechaRequest request) 
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Casos Historicos por Fecha e IDC, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoHistoricoByIdcFechaAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        
        [HttpPost("GetCasoHistoricoByDbc")]
        public async Task<IActionResult> GetCasoHistoricoByDbc([FromBody] ClienteDbcRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Casos Historicos por Datos Basicos, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoHistoricoByDbcAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        
        [HttpPost("GetCasoHistoricoByDbcFecha")]
        public async Task<IActionResult> GetCasoHistoricoByDbcFecha([FromBody] ClienteDbcFechaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Casos Historicos por Fecha y Datos Basicos, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoHistoricoByDbcFechaAllAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }      
    }
}

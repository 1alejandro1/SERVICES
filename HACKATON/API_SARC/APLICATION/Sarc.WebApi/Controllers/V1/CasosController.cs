using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.DTOs.Caso;
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
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CasosController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICasoService _casoService;
        private JsonSerializerOptions options;

        public CasosController(ILoggerManager logger, ICasoService casoService)
        {
            _logger = logger;
            _casoService = casoService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }        
        
        [HttpPost("RegistrarCaso")]
        public async Task<IActionResult> RegistrarCaso([FromBody] CreateCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Enviando e-mail: Codigo caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.AddCasoAsync(request, requestId);
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
            var response = await _casoService.UpdateSolucionCasoAsync(request, requestId,false);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

       
        [HttpPost("ActualizarAsignacionCaso")]
        public async Task<IActionResult> UpdateAsignacionCaso([FromBody] UpdateAsignacionCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Actualizacion de Asignacion de caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.UpdateAsignacionCasoExpressAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        
        
        [HttpPost("ObtenerCaso")]
        public async Task<IActionResult> GetCaso([FromBody] GetCasoDTO request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Consulta de Caso, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _casoService.GetCasoAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }       
        
    }
}

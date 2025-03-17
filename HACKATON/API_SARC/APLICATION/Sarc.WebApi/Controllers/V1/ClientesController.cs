using BCP.CROSS.MODELS.Client;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BCP.CROSS.COMMON.Helpers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using System.Linq;

namespace Sarc.WebApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IClienteService _clienteService;
        private JsonSerializerOptions options;

        public ClientesController(ILoggerManager logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }

        [HttpPost("GetClienteByIdc")]
        public async Task<IActionResult> GetClienteByIdc([FromBody] GetClientByIdcRequest request)
        {
            //var claims = User.Claims.ToList();
            var matricula = request.Funcionario;// claims.FirstOrDefault(x => x.Type == "Matricula").Value;            
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {requestId}] - GetClienteByIdc: {JsonSerializer.Serialize(request, options)}");
            var response = await _clienteService.GetClienteByIdcAsync(request, matricula, requestId);
            _logger.Information($"[ResponseId: {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("GetClienteByDbc")]
        public async Task<IActionResult> GetClienteByDbc([FromBody] GetClientByDbcRequest request)
        {           
            string requestId = Generator.RequestId(Request.Headers["Correlation-Id"]);

            _logger.Information($"[RequestId: {requestId}] - GetClienteByDbc: {JsonSerializer.Serialize(request, options)}");
            var response = await _clienteService.GetClienteByDbcAsync(request, requestId);
            _logger.Information($"[ResponseId: {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

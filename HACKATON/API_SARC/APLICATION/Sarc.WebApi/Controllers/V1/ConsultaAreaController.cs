using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.ConsultaArea;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace Sarc.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConsultaAreaController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IConsultaAreaService _sarcService;
        private JsonSerializerOptions options;

        public ConsultaAreaController(ILoggerManager logger, IConsultaAreaService sarcService)
        {
            _logger = logger;
            _sarcService = sarcService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }       
        
       [HttpPost("GetAreasByFuncionario")]
        public async Task<IActionResult> GetAreasByFuncionario([FromBody] FuncionarioRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Areas Analista");
            var response = await _sarcService.GetAreaByFuncionarioAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("GetAreasByFuncionarioRespuesta")]
        public async Task<IActionResult> GetAreasByFuncionarioRespuesta([FromBody] FuncionarioRespuestaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Areas Analista");
            var response = await _sarcService.GetAlertaAreaRespuestaAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("GetAreasByCaso")]
        public async Task<IActionResult> GetConsultaAreaByCaso([FromBody] GetCasoDTORequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Consulta Areas");
            var response = await _sarcService.GetConsultaAreaByCartaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateCasoRespuestaArea")]
        public async Task<IActionResult> UpdateCasoRespuestaArea([FromBody] UpdateCasoRespuestaArea request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Respuesta del Area, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _sarcService.UpdateCasoRespuestaAreaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

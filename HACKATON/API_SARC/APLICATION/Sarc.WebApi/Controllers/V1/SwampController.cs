using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Swamp;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace Sarc.WebApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SwampController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ISwampService _swampService;
        readonly IConfiguration _configuration;

        public SwampController(ILoggerManager logger, ISwampService swampService, IConfiguration configuration)
        {
            _logger = logger;
            _swampService = swampService;
            _configuration = configuration;
        }

        [HttpPost("GetDatosBasicosTicket")]
        public async Task<IActionResult> GetDatosBasicosTicket([FromBody] DatosBasicosTicketRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener datos del cliente");
            var response = await _swampService.GetDatosBasicosTicketAsync(request,requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("InsertEvent")]
        public async Task<IActionResult> InsertEvent([FromBody] InsertEventoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registrar datos de la sesion");
            var response = await _swampService.InsertEventAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
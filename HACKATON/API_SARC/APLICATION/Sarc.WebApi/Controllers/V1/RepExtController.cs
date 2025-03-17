using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.RepExt;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace Sarc.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RepExtController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepExtService _viasNotificationService;

        public RepExtController(ILoggerManager logger, IRepExtService viasNotificationService)
        {
            _logger = logger;
            _viasNotificationService = viasNotificationService;
        }

        [HttpPost("ValidaCuenta")]
        public async Task<IActionResult> ValidaCuenta([FromBody] ValidaCuentaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Validar Cuenta");
            var response = await _viasNotificationService.ValidarCuentaAsync(request,requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        /*
        [HttpPost("ListaClienteByCuenta")]
        public async Task<IActionResult> ListaClienteByCuenta([FromBody] GetClienteByCuentaRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener DatosCliente por Cuenta");
            var response = await _viasNotificationService.GetDatosCuentaAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        */
    }
}

using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.WcfSwamp;
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
    public class WcfSwampController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IWcfSwampService _wcfSwampkService;
        readonly IConfiguration _configuration;

        public WcfSwampController(ILoggerManager logger, IWcfSwampService wcfSwampService, IConfiguration configuration)
        {
            _logger = logger;
            _wcfSwampkService = wcfSwampService;
            _configuration = configuration;
        }

        [HttpPost("Abono")]
        public async Task<IActionResult> RealizarAbono([FromBody] DevolucionPR request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Realizar Abono");
            var response = await _wcfSwampkService.RealizarAbonoAsync(request,requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("Debito")]
        public async Task<IActionResult> Realizarcobro([FromBody] CobroPR request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Realizar Cobro");
            var response = await _wcfSwampkService.RealizarCobroAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
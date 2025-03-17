using BCP.CROSS.COMMON.Helpers;
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
    public class SmartLinkController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ISmartLinkService _smartLinkService;
        readonly IConfiguration _configuration;

        public SmartLinkController(ILoggerManager logger, ISmartLinkService smartLinkService, IConfiguration configuration)
        {
            _logger = logger;
            _smartLinkService = smartLinkService;
            _configuration = configuration;
        }

        [HttpPost("GetTipoCambio")]
        public async Task<IActionResult> GetTipoCambio()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener tipo cambio");
            var response = await _smartLinkService.GetTipoCambioAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
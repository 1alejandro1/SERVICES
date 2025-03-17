using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
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
    public class SharepointController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ISharepointService _sharepointService;
        readonly IConfiguration _configuration;

        public SharepointController(ILoggerManager logger, ISharepointService sharepointService, IConfiguration configuration)
        {
            _logger = logger;
            _sharepointService = sharepointService;
            _configuration = configuration;
        }

        [HttpPost("InsertArchivosAdjuntos")]
        public async Task<IActionResult> InsertArchivosAdjuntos([FromBody] ArchivosAdjuntos request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Insertar archivos adjuntos");
            var response = await _sharepointService.GuardarArchivosAdjuntosAsync(request,requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
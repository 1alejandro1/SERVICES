using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.Framework.Logs;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ViasNotificacionController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IViasNotificationService _viasNotificationService;

        public ViasNotificacionController(ILoggerManager logger, IViasNotificationService viasNotificationService)
        {
            _logger = logger;
            _viasNotificationService = viasNotificationService;
        }

        [HttpPost("ObtenerViasNotificacion")]
        public async Task<IActionResult> GetViasNotificacion()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Vias de Notificacion");
            var response = await _viasNotificationService.GetViasNotificacionAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

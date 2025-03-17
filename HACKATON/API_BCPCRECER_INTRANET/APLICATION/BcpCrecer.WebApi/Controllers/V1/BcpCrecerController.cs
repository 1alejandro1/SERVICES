using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Crecer;
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
using BCP.CROSS.MODELS.DTOs.Crecer;

namespace BcpCrecer.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BcpCrecerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICrecerService _crecerService;

        public BcpCrecerController(ILoggerManager logger, ICrecerService crecerService)
        {
            _logger = logger;
            _crecerService = crecerService;
        }

        [HttpPost("GetCategorias")]
        public async Task<IActionResult> GetCategorias()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Categorías");
            var response = await _crecerService.GetCategoriasAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
        [HttpPost("GetEmpresasByCategoriaCiudad")]
        public async Task<IActionResult> ObtieneEmpresasByCategoriaCiudad([FromBody] ObtieneEmpresasByCategoriaCiudadRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Consulta Empresas");
            var response = await _crecerService.ObtieneEmpresasByCategoriaCiudadAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

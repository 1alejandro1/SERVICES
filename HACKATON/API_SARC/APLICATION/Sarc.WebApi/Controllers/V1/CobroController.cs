using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Cobro;
using BCP.CROSS.MODELS.Common;
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
    public class CobroController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICobroService _cobroService;

        public CobroController(ILoggerManager logger, ICobroService cobroService)
        {
            _logger = logger;
            _cobroService = cobroService;
        }

        [HttpPost("ListaCobroByPR")]
        public async Task<IActionResult> ListaCobroByPR([FromBody] ProductoServicioRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Devoluciones Servicios Swamp");
            var response = await _cobroService.ListaCobroByPRAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("GetCobroListAll")]
        public async Task<IActionResult> GetCobroListAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Get Datos Cobro");
            var response = await _cobroService.GetCobroListAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("InsertCobro")]
        public async Task<IActionResult> InsertCobro([FromBody] CobroRegistro request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registra Cobro");
            var response = await _cobroService.InsertCobroAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateCobro")]
        public async Task<IActionResult> UpdateCobro([FromBody] CobroModificacion request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Modificar Cobro");
            var response = await _cobroService.UpdateCobroAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("DeshabilitarCobro")]
        public async Task<IActionResult> DeshabilitarCobro([FromBody] CobroDeshabilitar request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Deshabilitar cobro");
            var response = await _cobroService.DeshabilitarCobroAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

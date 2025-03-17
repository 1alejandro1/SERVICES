using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Funcionario;
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

namespace BcpCrecer.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IUsuarioService _sarcService;
        private JsonSerializerOptions options;
        public UsuarioController(ILoggerManager logger, IUsuarioService sarcService)
        {
            _logger = logger;
            _sarcService = sarcService;
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
        }

        [HttpPost("ListaUsuarioByCargo")]
        public async Task<IActionResult> GetUsuarioAllByTipoCargo([FromBody] CargoUsuarioRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Lista de Usuario por Tipo de Cargo");
            var response = await _sarcService.GetUsuarioAllByCargoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("InsertUsuario")]
        public async Task<IActionResult> InsertUsuario([FromBody] UsuarioRegistro request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registrar Usuario");
            var response = await _sarcService.InsertUsuarioAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateUsuario")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioModificacion request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Modificar Usuario");
            var response = await _sarcService.UpdateUsuarioAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("DatosUsuarioActivoByMatricula")]
        public async Task<IActionResult> GetFuncionarioAreaByMatricula([FromBody] MatriculaArea request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Information($"[RequestId: {channel} {requestId}] - Lista de Consulta Funcionarios-Areas, [Request]: {JsonSerializer.Serialize(request, options)}");
            var response = await _sarcService.GetDatosFuncionarioAreaAsync(request, requestId);
            _logger.Information($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response, options)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

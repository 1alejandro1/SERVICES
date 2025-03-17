using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Especialidad;
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
    public class EspecialidadController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadController(ILoggerManager logger, IEspecialidadService especialidadService)
        {
            _logger = logger;
            _especialidadService = especialidadService;
        }

        [HttpPost("ListaEspecialidad")]
        public async Task<IActionResult> GetAlertTipoCasoAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Especialidades");
            var response = await _especialidadService.GetTipoCasoAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ListaTiempoEspecialidad")]
        public async Task<IActionResult> GetTipoCasoAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Especialidades");
            var response = await _especialidadService.GetTipoCasoTiemposAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("InsertEspecialidad")]
        public async Task<IActionResult> RegistroTipoCaso([FromBody] TipoCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registrar Especialidades");
            var response = await _especialidadService.InsertTipoCasoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("InsertFuncionarioEspecialidad")]
        public async Task<IActionResult> InsertFuncionarioEspecialidad([FromBody] Especialidad request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registro de un funcionario para una especialidad");
            var response = await _especialidadService.InsertFuncionarioEspecialidadAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }     

        [HttpPost("UpdateEspecialidad")]
        public async Task<IActionResult> ModificarTipoCaso([FromBody] TipoCasoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Modificar Especialidades");
            var response = await _especialidadService.UpdateTipoCasoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("DeleteFuncionarioEspecialidad")]
        public async Task<IActionResult> DeleteFuncionarioEspecialidad([FromBody] FuncionarioEspecialidadRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Eliminacion del registro de un funcionario para una especialidad");
            var response = await _especialidadService.DeleteFuncionarioEspecialidadAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

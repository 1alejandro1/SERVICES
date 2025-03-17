using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.ProductoServicio;
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
    public class ProductoServicioController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IProductoServicioService _productoServicioService;

        public ProductoServicioController(ILoggerManager logger, IProductoServicioService productoServicioService)
        {
            _logger = logger;
            _productoServicioService = productoServicioService;
        }

        [HttpPost("ListaProductosActivos")]
        public async Task<IActionResult> ListaProductosAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Productos");
            var response = await _productoServicioService.GetProductoActivosAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ListaProductos")]
        public async Task<IActionResult> GetListProductsAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Productos");
            var response = await _productoServicioService.GetProductoAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

       [HttpPost("InsertProducto")]
        public async Task<IActionResult> InsertProducto([FromBody] Producto request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registrar Productos");
            var response = await _productoServicioService.InsertProductoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateProducto")]
        public async Task<IActionResult> UpdateProducto([FromBody] Producto request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Modificar Productos");
            var response = await _productoServicioService.UpdateProductoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ListaProductoServicio")]
        public async Task<IActionResult> ProductoServicioAll()
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener relacion Productos-Servicios");
            var response = await _productoServicioService.GetProductoServicioAllAsync(requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ListaServicioByEstadoTipo")]
        public async Task<IActionResult> GetServicioByTipoEstadoAsync([FromBody] TipoServicioEstadoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Producto por Estado");
            var response = await _productoServicioService.GetServicioByProductoTipoEstadoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ListaServicioByProductoTipo")]
        public async Task<IActionResult> GetServicioByProductoTipo([FromBody] TipoServicioProductoRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Areas");
            var response = await _productoServicioService.GetServicioByProductoTipoAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("InsertServicio")]
        public async Task<IActionResult> InsertServicio([FromBody] ServicioRegistroRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Registrar Servicio");
            var response = await _productoServicioService.InsertServicioAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateServicio")]
        public async Task<IActionResult> UpdateServicio([FromBody] ServicioModificacionRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Modificar Servicio");
            var response = await _productoServicioService.UpdateServicioAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }

            return Ok(response);
        }

    }
}

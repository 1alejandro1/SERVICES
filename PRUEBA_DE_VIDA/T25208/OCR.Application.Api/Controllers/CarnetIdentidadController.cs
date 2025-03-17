using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR.Application.Api.DTOs;
using OCR.Application.Api.Services;
using OCR.Application.Api.Utils;
using System;
using System.Threading.Tasks;

namespace OCR.Application.Api.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CarnetIdentidadController : ControllerBase
    {
        private readonly ICarnetIdentidadService service;

        public CarnetIdentidadController(ICarnetIdentidadService service)
        {
            this.service = service;
        }
//        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("Validar")]
        public async Task<IActionResult> Validate(ValidarCarnetIdentidadRequest request)
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.Validar(operation, request.Tipo, request.Image);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }       
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("CI")]
        public async Task<IActionResult> ValidateCI(ValidarCarnetIdentidadRequest request)
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.ValidarCI(operation, request.Tipo, request.Image);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }       
    }
}

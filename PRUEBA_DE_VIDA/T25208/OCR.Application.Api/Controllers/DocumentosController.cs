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
    public class DocumentosController : ControllerBase
    {
        private readonly IDocumentosService service;
        public DocumentosController(IDocumentosService service)
        {
            this.service = service;
        }
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("ValidarDoc")]
        public async Task<IActionResult> ValidateDoc()
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.ValidarDoc(operation, "");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("Seguro")]
        public async Task<IActionResult> ValidateSeguro(ValidarDocumentoRequest request)
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.ValidarSeguro(operation, request.image);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("Formulario")]
        public async Task<IActionResult> ValidateFormulario(ValidarDocumentoRequest request)
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.ValidarFormulario(operation, request.image, request.tipo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("ASFICirculares")]
        public async Task<IActionResult> ValidateASFI(ValidarDocumentoRequest request)
        {
            try
            {
                string operation = OperationUtil.GetOperation();
                var data = await service.ValidarASFI(operation, request.image);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

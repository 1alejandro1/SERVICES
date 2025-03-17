using BCP.CROSS.MODELS.Lexico;
using BCP.CROSS.MODELS.SharePoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sarc.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILexicoService _lexicoService;
        public FilesController(IFileService fileService, ILexicoService lexicoService)
        {
            _fileService = fileService;
            _lexicoService = lexicoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDevServicioCanalCuentaCon([FromBody] ServicioCanalCuentaRequest request)
        {
            var canalCuenta = new SelectListItem();
            var response = await _lexicoService.GetDevServicioCanalCuentaByProductoIdServivioIdAsync(request);
            if (response != null && response?.Count() > 0)
            {
                canalCuenta = new SelectListItem(response.First().CuentaContable, response.First().ServiciosCanales.ToString(), selected: true);
            }
            return Ok(canalCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> GetCobServicioCanalCuentaCon([FromBody] ServicioCanalCuentaRequest request)
        {
            var response = await _lexicoService.GetCobServicioCanalCuentaByProductoIdServivioIdAsync(request);
            if (request.CuentaSelected != default)
            {

                var servCanal = response.FirstOrDefault(x => x.ServiciosCanales.Equals(request.CuentaSelected));
                string monto = servCanal != null ? servCanal.Importe.ToString() : "";
                return Ok(monto);
            }
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> UploadSharePointAsync(ArchivosAdjuntosDTO request)
        {
            var response = await _fileService.UploadSharePointAsync(request);
            return Ok(response);
        }
        
    }
}

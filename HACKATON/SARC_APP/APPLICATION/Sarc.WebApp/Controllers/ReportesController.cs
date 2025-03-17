using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Reportes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models.Reportes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Analista = BCP.CROSS.MODELS.Reportes.Analista;
using ASFI = BCP.CROSS.MODELS.Reportes.ASFI;
using CNR = BCP.CROSS.MODELS.Reportes.CNR;

namespace Sarc.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;
        private readonly IAuthorizationService AuthorizationService;

        public ReportesController(IReportesService reportesService, IAuthorizationService authorizationService)
        {
            _reportesService = reportesService;
            AuthorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ASFI")]
        public IActionResult ASFI()
        {
            return View();

        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ASFI")]
        public async Task<IActionResult> ASFI(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<ASFI.SolucionadosModel>> res = new ServiceResponse<List<ASFI.SolucionadosModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            switch (request.ASFITipo)
            {
                case "Solucionados":
                    ServiceResponse<List<ASFI.SolucionadosModel>> getAFISolucionados = await _reportesService.GetASFISoluciones(request);
                    ViewBag.response = getAFISolucionados;
                    break;
                case "Registrados":
                    ServiceResponse<List<ASFI.RegistradosModel>> getASFIRegistrados = await _reportesService.GetASFIRegistrados(request);
                    ViewBag.response = getASFIRegistrados;
                    break;
                default:
                    break;
            }
            ViewBag.tipo = request.ASFITipo;
            return View();

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPANA")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ANACASSOL")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ANAESP")]
        public IActionResult Analista()
        {
            return View();

        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPANA")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ANACASSOL")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ANAESP")]
        public async Task<IActionResult> Analista(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            switch (request.AnalistaTipo)
            {
                case "CantidadCaso":
                    ServiceResponse<List<Analista.CantidadCasosModel>> getCantidadCasos = await _reportesService.GetAnalistaCantidadCasos(request);
                    ViewBag.response = getCantidadCasos;
                    break;
                case "CasosSolucionados":
                    ServiceResponse<List<Analista.CasosSolucionadosModel>> getCasosSolucionados = await _reportesService.GetAnalistaCasosSolucionados(request);
                    ViewBag.response = getCasosSolucionados;
                    break;
                case "Especialidad":
                    ServiceResponse<List<Analista.EspecialidadModel>> getEspecialidad = await _reportesService.GetAnalistaEspecialidad(request);
                    ViewBag.response = getEspecialidad;
                    break;
                default:

                    break;
            }
            ViewBag.tipo = request.AnalistaTipo;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPBAS")]
        public IActionResult ReporteBase()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPBAS")]
        public async Task<IActionResult> ReporteBase(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<ReporteBaseModel>> getData = await _reportesService.GetReporteBase(request);
            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ESPCAPINT")]
        public IActionResult CapacidadEspecialidad()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_ESPCAPINT")]
        public async Task<IActionResult> CapacidadEspecialidad(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<CapacidadEspecialidadModel>> getData = await _reportesService.GetCapacidadEspecialidad(request);
            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPCNR")]
        public IActionResult CNR()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPCNR")]
        public async Task<IActionResult> CNR(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            if (request.CNRTipo == "Total")
            {
                ServiceResponse<List<CNR.TotalModel>> getData = await _reportesService.GetCNRTotal(request);
                ViewBag.response = getData;
                ViewBag.tipo = request.CNRTipo;
            }
            else
            {
                ServiceResponse<List<CNR.DetalleModel>> getData = await _reportesService.GetCNRDetalle(request);
                ViewBag.response = getData;
                ViewBag.tipo = request.CNRTipo;
            }
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPTIPRECSER")]
        public IActionResult TipoReclamo()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPTIPRECSER")]
        public async Task<IActionResult> TipoReclamo(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<TipoReclamoModel>> getData = await _reportesService.GetTipoReclamo(request);
            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPPROSER")]
        public IActionResult ReposicionTarjeta()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REPPROSER")]
        public async Task<IActionResult> ReposicionTarjeta(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<ReposicionTarjetaModel>> getData = await _reportesService.GetReposicionTarjeta(request);
            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_DEVATMPOS")]
        public IActionResult DevolucionATMPOS()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_DEVATMPOS")]
        public async Task<IActionResult> DevolucionATMPOS(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<DevolucionesATMPOSModel>> getData = await _reportesService.GetDevolucionATMPOS(request);
            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_EXP")]
        public IActionResult Expedicion()
        {
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_EXP")]
        public async Task<IActionResult> Expedicion(ReportesFormViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            ServiceResponse<List<ExpedicionModel>> getData = await _reportesService.GetExpedicion(request);

            ViewBag.response = getData;
            return View();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REP_COB_DEV")]
        public IActionResult CobrosDevoluciones()
        {
            bool pr = AuthorizationService.AuthorizeAsync(User, "SARC_MOD_REP_COB_DEV_PR").Result.Succeeded;
            bool SW = AuthorizationService.AuthorizeAsync(User, "SARC_MOD_REP_COB_DEV_SW").Result.Succeeded;
            ViewBag.Canal = "todos";
            if (pr == true && SW == false)
            {
                ViewBag.Canal = "pr";
            }
            if (pr == false && SW == true)
            {
                ViewBag.Canal = "sw";
            }
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "SARC_MOD_REP_COB_DEV")]

        public async Task<IActionResult> CobrosDevoluciones(CobrosDevolucionesViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Meta meta = new Meta();
                ServiceResponse<List<TipoReclamoModel>> res = new ServiceResponse<List<TipoReclamoModel>>();
                res.Meta.Msj = "Introduzca los datos correctos.";
                res.Data = null;
                ViewBag.response = res;
                return View();
            }
            
            ServiceResponse<List<CobrosDevolucionesModel>> getData = await _reportesService.GetCobrosDevoluciones(request);
            ViewBag.response = getData;
            return View();
        }
    }
}

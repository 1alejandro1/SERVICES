using MICREDITO.BusinessLogic;
using MICREDITO.Model.Service;
using MICREDITO.WebSite.Controllers.Base;
using MICREDITO.WebSite.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MICREDITO.WebSite.Controllers
{
    [CustomAuthorization("Login", "Index")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.busqueda = new BusquedaModel();
            BandejaService service = new BandejaService();
            List<BandejaResponseModel> _model = service.Bandeja(user.matricula);
            model.bandeja = _model;
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(HomeModel model)
        {
            BandejaService service = new BandejaService();
            if (string.IsNullOrEmpty(model.busqueda.nroEvaluacion))
                model.busqueda.nroEvaluacion = string.Empty;
            if (string.IsNullOrEmpty(model.busqueda.cliente))
                model.busqueda.cliente = string.Empty;
            if (string.IsNullOrEmpty(model.busqueda.nroIDC))
                model.busqueda.nroIDC = string.Empty;
            List<BandejaResponseModel> _model = service.Busqueda(model.busqueda.nroEvaluacion.ToUpper(), model.busqueda.cliente.ToUpper(), model.busqueda.nroIDC.ToUpper());
            model.bandeja = _model;
            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
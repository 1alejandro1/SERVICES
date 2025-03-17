using System.Web.Mvc;

namespace MICREDITO.WebSite.Controllers
{
    public class ErrorControl
    {
        public int error { get; set; }
        public string exception { get; set; }
    }

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ErrorControl error = Session["error"] as ErrorControl;
            switch (error.error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.Title = "Lo sentimos, esta página no existe";
                    ViewBag.Description = "La página que estás buscando podría haber sido eliminada o jamás haber existido";
                    ViewBag.Redirection = true;
                    break;

                default:
                    ViewBag.Title = "Un error Inesperado";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    ViewBag.Controlado = true;
                    ViewBag.Details = error.exception;
                    break;
            }

            return View();
        }
    }
}
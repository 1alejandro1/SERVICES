using MICREDITO.Authentication;
using MICREDITO.WebSite.Models;
using System.Web.Mvc;

namespace MICREDITO.WebSite.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            UserMain.LogOut();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            string Message = string.Empty;
            if (string.IsNullOrEmpty(model.usuario))
            {
                model.password = string.Empty;
                ViewBag.Mensaje = "EL CAMPO USUARIO SE ENCUENTRA VACIO.";
                return View("Index", model);
            }
            if (string.IsNullOrEmpty(model.password))
            {
                model.password = string.Empty;
                ViewBag.Mensaje = "EL CAMPO CONTRASEÑA SE ENCUENTRA VACIO.";
                return View("Index", model);
            }
            if (UserMain.LogIn(model.usuario.ToUpper(), model.password, out Message))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.password = string.Empty;
                ViewBag.Mensaje = Message.ToUpper();
                return View("Index", model);
            }
        }

        public ActionResult Logout()
        {
            LoginModel model = new LoginModel();
            UserMain.LogOut();
            return View("Index", model);
        }
    }
}
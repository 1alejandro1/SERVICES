using MICREDITO.Authentication;
using MICREDITO.Model.Service;
using MICREDITO.WebSite.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MICREDITO.WebSite.Controllers.Base
{
    [HandleError()]
    [CustomAuthorization("Login", "Index")]
    public class BaseController : Controller
    {
        protected LoginResponseModel user;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var main = (UserMain)User;
            var identity = (UserIdentity)main.Identity;
            this.user = identity.Data;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName + "_MICREDITO"];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var newTicket = FormsAuthentication.RenewTicketIfOld(ticket);
                if (newTicket.Expiration != ticket.Expiration)
                {
                    var encryptedTicket = FormsAuthentication.Encrypt(newTicket);
                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName + "_MICREDITO", encryptedTicket);
                    Response.Cookies.Add(cookie);
                }
                UserMain.LogIn(ticket.UserData);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            base.OnException(filterContext);
        }
    }
}
using System.Web.Mvc;
using System.Web.Routing;

namespace MICREDITO.WebSite.Models
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        private string controller;
        private string action;

        public CustomAuthorization(string controller, string action)
        {
            this.controller = controller;
            this.action = action;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = controller,
                    action = action
                }));
            }
        }
    }
}
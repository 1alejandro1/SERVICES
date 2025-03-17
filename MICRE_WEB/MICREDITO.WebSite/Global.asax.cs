using Autofac;
using Autofac.Integration.Mvc;
using MICREDITO.WebSite.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MICREDITO.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacRegister();
        }

        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;
            int error = httpException != null ? httpException.GetHttpCode() : 0;
            Server.ClearError();
            ErrorControl _error = new ErrorControl();
            _error.error = error;
            _error.exception = exception.Message;
            Session["error"] = _error;
            Response.Redirect(string.Format("~/Error"), false);
        }
    }
}

using System.Diagnostics;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sarc.WebApp.Models;

namespace Sarc.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerManager _logger;

        public HomeController(ILoggerManager logger)
        {
            _logger = logger;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Sarc/Error/{code:int}")]
        public IActionResult Error(int code)
        {
            if (code == 404)
            {
                return this.View("~/Views/Shared/NotFound.cshtml");
            }
            else
            {
                return this.View("~/Views/Shared/Error.cshtml", new ErrorViewModel { CorrelationId = Activity.Current?.RootId ?? this.HttpContext.TraceIdentifier });
            }
        }
    }
}
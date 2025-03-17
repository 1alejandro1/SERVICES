using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Segurinet;
using BCP.Framework.Logs;
using Card_App.Contratcts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOptions<SegurinetApiSettings> _segurinetApiSettings;
        private readonly IOptions<SegurinetSettings> _segurinetSettings;
        private readonly ILoggerManager _logger;
        public LoginController(IAuthService authService, ILoggerManager logger, IOptions<SegurinetApiSettings> segurinetApiSettings, IOptions<SegurinetSettings> segurinetSettings)
        {
            _authService = authService;
            _segurinetApiSettings = segurinetApiSettings;
            _segurinetSettings = segurinetSettings;

            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Login(ApiSegurinetRequest loginRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(loginRequest);
                }

                var listPolitics = _segurinetSettings.Value.Politicas;
                List<string> lstPolicies = new List<string>();

                foreach (var politic in listPolitics)
                {
                    lstPolicies.Add(politic.nombre);
                }
                loginRequest.lstPolicies = lstPolicies;
                loginRequest.application = _segurinetApiSettings.Value.Application;

                loginRequest.user = loginRequest.user.ToUpper();
                var loginResponse = await _authService.LoginAsync(loginRequest);
                loginResponse.matricula = loginRequest.user;
                if (loginResponse.lstPolicies.Count > 0)
                {
                    HttpContext.Session.Set<ApiSegurinetResponse>("cardtkn", loginResponse);
                    return RedirectToAction("Index", "Home");
                }
                loginRequest.Msj = loginResponse?.message;
                return View(loginRequest);

            }
            catch (Exception ex)
            {
                _logger.Error("Error en Controller Login(): ", ex);
                return null;
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Login/Login");
        }

    }
}

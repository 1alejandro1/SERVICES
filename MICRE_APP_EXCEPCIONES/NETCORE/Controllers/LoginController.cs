using MICRE_APP_EXCEPCIONES.SessionHandler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using NETCORE.Services.Interfaces;
using Newtonsoft.Json;

namespace NETCORE.Controllers
{

    [Route("account")]
    public class LoginController : Controller
    {
        private readonly SessionHandler _sessionHandler;
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _service;

        public LoginController(ILogger<HomeController> logger, ILoginService service, SessionHandler sessionHandler)
        {
            _logger = logger;
            _sessionHandler = sessionHandler;
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User userCed)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (userCed == null || string.IsNullOrEmpty(userCed.UserName) || string.IsNullOrEmpty(userCed.Password))
            {
                ViewBag.ErrorMessage = "Invalid user data.";
                return View();
            }
            try
            {
                var token = await _service.GetToken(userCed);
                
                if (token.Message == "La operación se ha ejecutado con éxito.") 
                {
                    await _sessionHandler.SignInAsync(userCed.UserName);

                    HttpContext.Session.SetString("token", JsonConvert.SerializeObject(token.Data));
                    return RedirectToAction("SolicitudTable", "Solicitud");
                }
                else
                {
                    ViewBag.ErrorMessage = token.Message;
                    return View();
                }
                
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.Message;
                ViewBag.ErrorMessage = $"{statusCode}";
                return View();
            }
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        
        [HttpGet]
        [Route("check")]
        public async Task<IActionResult> CheckSession()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Login");
            }
            return Ok();
        }
    }
}

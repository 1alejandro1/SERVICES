using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Segurinet;
using Card_App.Contratcts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOptions<SegurinetApiSettings> _segurinetApiSettings;
        private readonly IOptions<SegurinetSettings> _segurinetSettings;


        public AuthController(IAuthService authService, IOptions<SegurinetApiSettings> segurinetApiSettings, IOptions<SegurinetSettings> segurinetSettings)
        {
            _authService = authService;
            _segurinetApiSettings = segurinetApiSettings;
            _segurinetSettings = segurinetSettings;
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}

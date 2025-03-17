using Microsoft.AspNetCore.Mvc;
using BCP.CROSS.MODELS;
using Microsoft.AspNetCore.Http;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Login;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }
        
    [HttpPost()]
    public async  Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(loginRequest);
        }

        loginRequest.Usuario = loginRequest.Usuario.ToUpper();
        var loginResponse = await _authService.LoginAsyncTest(loginRequest);
        if (loginResponse.Meta.StatusCode == 200)
        {
            HttpContext.Session.Set<LoginResponse>("sarctkn", loginResponse.Data);
            return RedirectToAction("Index", "Home");
        }

        loginRequest.Msj = loginResponse?.Meta?.Msj;
        return View(loginRequest);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("~/Auth/Login");
    }

    public IActionResult Forbidden()
    {
        return View();
    }
}


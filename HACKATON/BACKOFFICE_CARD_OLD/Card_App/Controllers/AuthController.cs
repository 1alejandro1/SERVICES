using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Segurinet;
using Card_App.Contratcts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost()]
    public async Task<IActionResult> Login(ApiSegurinetRequest loginRequest)
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


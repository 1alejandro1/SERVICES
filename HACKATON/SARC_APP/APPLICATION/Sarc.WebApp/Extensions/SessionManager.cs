using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Login;

namespace Sarc.WebApp.Extensions
{
    public class SessionManager: Controller
    {
        public string ReadSession(string name)
        {
            HttpContext.Session.SetString("sarctkn", "the doc");
            var tk = HttpContext.Session.GetString(name);
            if (HttpContext.Session.GetString(name) == default)
            {
                return string.Empty;
            }
            var token = HttpContext.Session.Get<string>(name);
            return token;
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using BCP.CROSS.COMMON.Helpers;
using Microsoft.AspNetCore.Mvc;
using BCP.CROSS.MODELS.Login;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Sarc.WebApp.Extensions
{
    public class AuthenticationHandlerManager : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;
        public AuthenticationHandlerManager(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var data = _session.Get<LoginResponse>("sarctkn");

                if (data is null)
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(data?.Token?.Token);
                var expiration = decodedValue.ValidTo.CompareTo(DateTime.Now);

                if (expiration < 0)
                {
                    return AuthenticateResult.Fail("Token expirado");
                }

                string user = await Task.Run(() => decodedValue.Claims.FirstOrDefault(x => x.Type == "Matricula").Value);
                string nombre = await Task.Run(() => decodedValue.Claims.FirstOrDefault(x => x.Type == "Nombre").Value);
                var roles = await Task.Run(() => decodedValue.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).ToList());

                var claims = new List<Claim>{
                    new Claim(JwtRegisteredClaimNames.UniqueName, user),
                    new Claim(ClaimTypes.Name, user),
                    new Claim("Nombre", nombre)
                };

                foreach (var rol in roles)
                {
                    claims.Add(new Claim("Role", rol));
                }

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                _httpContextAccessor.HttpContext.User = principal;

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

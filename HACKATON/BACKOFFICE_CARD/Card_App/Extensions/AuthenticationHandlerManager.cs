using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS.Login;
using BCP.CROSS.MODELS.Segurinet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Card_App.Extensions
{
    public class AuthenticationHandlerManager : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;
        private readonly IOptions<SegurinetSettings> _segurinetSettings;
        public AuthenticationHandlerManager(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<SegurinetSettings> segurinetSettings,
            IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _segurinetSettings = segurinetSettings;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var data = _session.Get<ApiSegurinetResponse>("cardtkn");

                if (data is null)
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
                var handler = new JwtSecurityTokenHandler();
                //var decodedValue = handler.ReadJwtToken(data?.Token?.Token);
                //var expiration = decodedValue.ValidTo.CompareTo(DateTime.Now);

                //if (expiration < 0)
                //{
                //    return AuthenticateResult.Fail("Token expirado");
                //}

                string user = "";
                string nombre = "";
                var listPolitics = _segurinetSettings.Value.Politicas;
              

                var claims = new List<Claim>{
                    new Claim(JwtRegisteredClaimNames.UniqueName, user),
                    new Claim(ClaimTypes.Name, user),
                    new Claim("Nombre", nombre)
                };

                foreach (var rol in listPolitics)
                {
                    claims.Add(new Claim("Role", rol.roles));
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

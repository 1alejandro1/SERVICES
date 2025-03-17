using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BCP.CROSS.SECURITY.BasicAuthentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IBasicAuthenticationService _authService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicAuthenticationService authService)
            : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");


            try
            {
                AuthorizationResponse user = null;
                var headers = BuilHeaderRequest(Request);

                user = await _authService.Validate(headers);

                if (user.Data == null)
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }

               /* List<string> roles = new List<string>();
                try
                {
                    var privateToken = new JwtSecurityTokenHandler().ReadJwtToken(headers.ChanelAuthorized.PrivateToken);
                    var tokenRoles = privateToken.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                    foreach (var z in tokenRoles)
                    {
                        roles.Add(z.Value);
                    }
                }
                catch (Exception) { }*/
                var claims = new List<Claim>(); 
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, headers.Username));
               /* for(int i=0;i<roles.Count; i++)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles[i]));
                }*/
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {
                throw;
            }



        }
        private static AuthorizationRequest BuilHeaderRequest(HttpRequest headerRequest)
        {
            var authHeader = AuthenticationHeaderValue.Parse(headerRequest.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

            return new AuthorizationRequest
            {
                Username = credentials[0],
                Password = credentials[1],
                ChanelAuthorized = new ChanelAuthorized
                {
                    Date = DateTime.Now.ToString("yyyyyddmmhhss"),
                    Channel = headerRequest.Headers["channel"],
                    PublicToken = headerRequest.Headers["publicToken"],
                    AppUserId = headerRequest.Headers["appUserId"],
                    PrivateToken= headerRequest.Headers["privateToken"]
                }
            };

        }

    }
}

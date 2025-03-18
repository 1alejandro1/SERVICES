using BCP.CROSS.MODELS.Response;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RecognitionPasiva.App.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace RecognitionPasiva.App.Services
{
    public interface ILoginServices 
    {
        Task Login(string userToken);
        Task Logout();
    }
    public class LoginServices: AuthenticationStateProvider, ILoginServices
    {
        public static readonly string TOKENKEY = "TOKENKEY";

        private readonly IJSRuntime js;
        private readonly LoginSesion appDataSharedService;
        private readonly IGatewayServices service;

        public LoginServices(IJSRuntime js, LoginSesion appDataSharedService, IGatewayServices service)
        {
            this.js = js;
            this.appDataSharedService = appDataSharedService;
            this.service = service;
        }

        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromSessionStorage(TOKENKEY);
            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }
            if (TokenExpirado(token))
            {
                await Limpiar();
                return Anonimo;
            }
            return await LLenarData(token);
        }

        private async Task<AuthenticationState> LLenarData(string token)
        {
            appDataSharedService.UserAgent=await js.GetUserAgent();
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            var values = decodedValue?.Claims;
            var userId = values?.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"))?.Value;
            appDataSharedService.UserId = userId ?? String.Empty;
            var channelId = values?.FirstOrDefault(x => x.Type.Equals("nameid"))?.Value;
            appDataSharedService.ChannelId = channelId == null ? -1 : int.Parse(channelId);
            var ci = values?.FirstOrDefault(x => x.Type.Equals("unique_name"))?.Value;
            appDataSharedService.CI = ci ?? String.Empty;
            var parameter = await service.Parameter(appDataSharedService.UserId, appDataSharedService.ChannelId);
            if (!parameter.Error)
            {
                appDataSharedService.parameters = parameter.Response.Data ?? new List<ParameterDataResponse>();
            }
            else
            {
                appDataSharedService.parameters = new List<ParameterDataResponse>();
            }
            return ConstruirAuthenticationState(token);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }
        private bool TokenExpirado(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            var timenw = DateTime.UtcNow;
            var expiration = decodedValue.ValidTo.CompareTo(DateTime.UtcNow);

            return expiration < 0;
        }
        public async Task Login(string userToken)
        {
            await js.SetInSessionStorage(TOKENKEY, userToken);    
            var authState = await LLenarData(userToken);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private async Task Limpiar()
        {
            await js.RemoveSessionItem(TOKENKEY);
        }
        #region PARSEAR JWT
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        #endregion
    }
}

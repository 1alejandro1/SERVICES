using MICRE.ABSTRACTION.ENTITIES.Options;
using MICRE.APPLICATION.CONNECTIONS.SERVICES.HttpConnectionServices;
using Microsoft.Extensions.Options;
using NETCORE.Models;
using NETCORE.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;
using MICRE.ABSTRACTION.LOGGER;

namespace NETCORE.Services
{
    public class LoginService : ILoginService
    {
        private readonly IHttpConnectionServices _httpConnection;
        private readonly ApiExcepcionesOptions _apiExcepcionesOptions;
        private readonly ILoggerInfo _logger;

        public LoginService(IHttpConnectionServices httpConnection, IOptions<ApiExcepcionesOptions> apiExcepcionesOptions, ILoggerInfo logger)
        {
            _httpConnection = httpConnection;
            _apiExcepcionesOptions = apiExcepcionesOptions.Value;
            _logger = logger;
        }

        public async Task<TokenResponse> GetToken(User user)
        {
            try
            {
                this._logger.Debug(string.Format("[{0}]      URL: {1}", string.Empty, this._apiExcepcionesOptions.urlApiExcepciones + this._apiExcepcionesOptions.methodUserAuthentication));
                var _response = await _httpConnection.PostAsync<TokenResponse>(_apiExcepcionesOptions.urlApiExcepciones, _apiExcepcionesOptions.methodUserAuthentication, new { }, Authentication.Basic, null, user.UserName ?? string.Empty, user.Password ?? string.Empty);
                this._logger.Debug(string.Format("[{0}] RESPONSE: {1}", string.Empty, JsonConvert.SerializeObject(_response)));
                return _response;
            }
            catch (Exception)
            {
                throw new HttpRequestException($"Ocurrio un error, vuelva a intentarlo.");
            }
        }
    }
}

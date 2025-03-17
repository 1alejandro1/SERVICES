using MICRE.ABSTRACTION.ENTITIES;
using MICRE.ABSTRACTION.ENTITIES.Options;
using MICRE.ABSTRACTION.LOGGER;
using MICRE.APPLICATION.CONNECTIONS.SERVICES.HttpConnectionServices;
using MICRE_APP_EXCEPCIONES.Models;
using MICRE_APP_EXCEPCIONES.Models.APIResponse;
using MICRE_APP_EXCEPCIONES.TDOs;
using Microsoft.Extensions.Options;
using NETCORE.Models;
using NETCORE.Models.APIResponse;
using NETCORE.Services.Interfaces;
using NETCORE.TDOs;
using Newtonsoft.Json;

namespace NETCORE.Services
{
    public class FormularioInmuebleService : IFormularioInmuebleService
    {
        private readonly IHttpConnectionServices _httpConnection;
        private readonly ApiExcepcionesOptions _apiExcepcionesOptions;
        private readonly ILoggerInfo _logger;

        public FormularioInmuebleService(IHttpConnectionServices httpConnection, IOptions<ApiExcepcionesOptions> apiExcepcionesOptions, ILoggerInfo logger)
        {
            _httpConnection = httpConnection;
            _apiExcepcionesOptions = apiExcepcionesOptions.Value;
            this._logger = logger;
        }

        public async Task<PersonaResponse> FindUser(ClienteIdc busqueda)
        {
            try
            {
                this._logger.Debug(string.Format("[{0}]      URL: {1}", string.Empty, this._apiExcepcionesOptions.urlApiExcepciones + this._apiExcepcionesOptions.methodBuscarCliente));
                this._logger.Debug(string.Format("[{0}]  REQUEST: {1}", string.Empty, JsonConvert.SerializeObject(busqueda)));
                var _response = await _httpConnection.PostAsync<PersonaResponse>(_apiExcepcionesOptions.urlApiExcepciones, _apiExcepcionesOptions.methodBuscarCliente, busqueda, Authentication.None);
                this._logger.Debug(string.Format("[{0}] RESPONSE: {1}", string.Empty, JsonConvert.SerializeObject(_response)));
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<FormInmuebleResponse> SubmitTitularForm(FormTitularTDO busqueda)
        {
            try
            {
                this._logger.Debug(string.Format("[{0}]      URL: {1}", string.Empty, this._apiExcepcionesOptions.urlApiExcepciones + this._apiExcepcionesOptions.methodRegistrarClienteExcepcion));
                this._logger.Debug(string.Format("[{0}]  REQUEST: {1}", string.Empty, JsonConvert.SerializeObject(busqueda)));
                var _response = await _httpConnection.PostAsync<FormInmuebleResponse>(_apiExcepcionesOptions.urlApiExcepciones, _apiExcepcionesOptions.methodRegistrarClienteExcepcion, busqueda, Authentication.None);
                this._logger.Debug(string.Format("[{0}] RESPONSE: {1}", string.Empty, JsonConvert.SerializeObject(_response)));
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }


        public async Task<ExceptionResponse> RegistrarException(RegisterExceptionTDO exception)
        {
            try
            {
                var _response = await _httpConnection.PostAsyncV2<ExceptionResponse>(_apiExcepcionesOptions.urlApiExcepciones, _apiExcepcionesOptions.methodRegistrarExcepcion, exception, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }


        public async Task<List<Excepcion>> GetException()
        {
            try
            {
                var _response = await _httpConnection.GetAsync<GenericResponse<List<Excepcion>>>(_apiExcepcionesOptions.urlApiExcepciones, _apiExcepcionesOptions.methodObtenerTipoExcepciones, Authentication.None);
                return _response.data ?? new();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }
        public async Task<RemoveExceptionResponse> PostRemoveException(string id)
        {
            int idInt = int.Parse(id);
            try
            {
                string _methodFull = $"{_apiExcepcionesOptions.methodRemoveExcepcion}?idProductoExcepcion={idInt}";
                var _response = await _httpConnection.PostAsync<RemoveExceptionResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, new { }, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<RemoveExceptionResponse> PostUpdateException(UpdateExceptionRequest update)
        {
            try
            {
                string _methodFull = _apiExcepcionesOptions.methodUpdateExcepcion;
                var _response = await _httpConnection.PostAsync<RemoveExceptionResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, update, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<RegisterRespladoResponse> PostRegisterBackUp(RegisterRespladoRequest backup)
        {
            try
            {
                string _methodFull = _apiExcepcionesOptions.methodAddbackup;
                var _response = await _httpConnection.PostAsync<RegisterRespladoResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, backup, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<RegisterRespladoResponse> PostRegisterBackupExcepcion(DocumentoRespaldoExcepcionRequest backup)
        {
            try
            {
                string _methodFull = _apiExcepcionesOptions.methodAddBackupExcepcion;
                var _response = await _httpConnection.PostAsync<RegisterRespladoResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, backup, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<BackUpsResponse> GetBackUps(string backup)
        {
            try
            {
                string _methodFull = $"{_apiExcepcionesOptions.methodGetbackups}?_idCliente={backup}";
                var _response = await _httpConnection.PostAsync<BackUpsResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, new { }, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);

            }
        }
        public async Task<ResponseModel> GetProductos(string combo)
        {
            try
            {
                string _methodFull = $"{_apiExcepcionesOptions.methodCombos}?_combo={combo}";
                var _response = await _httpConnection.PostAsync<ResponseModel>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, new { }, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<TipoCambioResponse> GetCambio()
        {
            try
            {
                string _methodFull = $"{_apiExcepcionesOptions.methodGetCambio}";
                var _response = await _httpConnection.GetAsync<TipoCambioResponse>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }

        public async Task<ExceptionIdRequest> GetExcepcionesByID(string idCliente)
        {
            try
            {
                string _methodFull = $"{_apiExcepcionesOptions.methodObtenerExcepciones}?idClienteExcepcion={idCliente}";
                var _response = await _httpConnection.PostAsync<ExceptionIdRequest>(_apiExcepcionesOptions.urlApiExcepciones, _methodFull, new { }, Authentication.None);
                return _response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Request error", e);
            }
        }
        
    }
}

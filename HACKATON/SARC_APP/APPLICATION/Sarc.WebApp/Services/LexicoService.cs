using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.Lexico;
using BCP.CROSS.MODELS.SmartLink;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sarc.WebApp.Services
{
    public class LexicoService : ILexicoService
    {
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;
        private readonly SarcApiConnector _sarcApiConnector;
        public LexicoService(
            IOptions<SarcApiSettings> sarcApiSettings,
            SarcApiConnector sarcApiConnector
        )
        {
            _sarcApiSettings = sarcApiSettings;
            _sarcApiConnector = sarcApiConnector;
        }

        public async Task<IEnumerable<ViasNotificacion>> GetViasNotificacionAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<ViasNotificacion>, string>(
                _sarcApiSettings.Value.Lexico.ViasNotificacion, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<Producto>, string>(
               _sarcApiSettings.Value.Lexico.Productos, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<ServicioByProductoTipoEstadoResponse>> GetServiciosAsync(ServicioByProductoTipoEstadoRequest request)
        {
            var response = await _sarcApiConnector.PostAsync<List<ServicioByProductoTipoEstadoResponse>, ServicioByProductoTipoEstadoRequest>(
              _sarcApiSettings.Value.Lexico.Servicios, request);

            return response.Data;
        }

        public async Task<IEnumerable<ValidaCuentaResponse>> ValidateCuentaAsync(string nroCuenta, string clienteIdc, string clienteIdcTipo, string clienteIdcExtension)
        {
            ValidaCuentaRequest request = new()
            {
                IDC = clienteIdc,
                IdcExtenion = clienteIdcExtension,
                IdcTipo = clienteIdcTipo,
                NroCuenta = nroCuenta
            };

            var response = await _sarcApiConnector.PostAsync<List<ValidaCuentaResponse>, ValidaCuentaRequest>(
             _sarcApiSettings.Value.Lexico.ValidaCuenta, request);

            return response.Data;
        }

        public async Task<IEnumerable<TipoIdc>> GetTipoIdc()
        {
            var tiposIdc = await Task.Run(() => _sarcApiSettings.Value.Lexico.TipoIdc);
            return tiposIdc;
        }

        public async Task<IEnumerable<Sucursals>> GetSucursalesAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<Sucursals>, string>(
            _sarcApiSettings.Value.Lexico.Sucursales, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<Atm>> GetAtmsAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<Atm>, string>(
           _sarcApiSettings.Value.Lexico.ATMs, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<Ciudad>> GetCidudadesAsync(string departamentoId)
        {
            var path = Path.Combine(_sarcApiSettings.Value.RutaRecursos, "Ciudades.json");
            using StreamReader rs = new(path);
            var content = await rs.ReadToEndAsync().ConfigureAwait(false);
            var ciudades = JsonSerializer.Deserialize<List<Ciudad>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var response = await Task.Run(() => ciudades.Where(x => x.DepartamentoId.Equals(departamentoId)).ToList());

            return response;
        }

        public async Task<IEnumerable<ParametrosResponse>> GetCentrosCostoAsync()
        {
            SwampLexicoRequest request = new (){ Lexico = "CENTRO" };
            
            var response = await _sarcApiConnector.PostAsync<List<ParametrosResponse>, SwampLexicoRequest>(
                _sarcApiSettings.Value.Lexico.ServiciosSwampLexico, request);

            return response.Data;
        }

        public async Task<IEnumerable<ParametroSarcResponse>> GetParametroSarcByTipoAsync(string lexico)
        {
            SwampLexicoRequest request = new (){ Lexico = lexico };
            
            var response = await _sarcApiConnector.PostAsync<List<ParametroSarcResponse>, SwampLexicoRequest>(
                _sarcApiSettings.Value.Lexico.ParametroSacrByTipo, request);

            return response.Data;
        }

        public async Task<IEnumerable<TipoSolucionResponse>> GetTiposSolucionAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<TipoSolucionResponse>, string>(
                _sarcApiSettings.Value.Lexico.TipoSolucion, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<Carta>> GetCartaAllAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<Carta>, string>(
                _sarcApiSettings.Value.Lexico.GetCartaAll, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<Area>> GetAreaAllAsync()
        {
            var response = await _sarcApiConnector.PostAsync<List<Area>, string>(
                _sarcApiSettings.Value.Lexico.GetAreaAll, string.Empty);

            return response.Data;
        }

        public async Task<IEnumerable<ProductoServicioResponse>> GetDocumentacionRequerida(ProductoServicioRequest request)
        {
            var response = await _sarcApiConnector.PostAsync<IEnumerable<ProductoServicioResponse>, ProductoServicioRequest>
                (_sarcApiSettings.Value.Lexico.DocumentacionAdjByProductoServicio, request);

            return response.Data;
        }

        public async Task<IEnumerable<DevolucionProductoServicioResponse>> GetDevServicioCanalCuentaByProductoIdServivioIdAsync(ServicioCanalCuentaRequest request)
        {
            var response = await _sarcApiConnector.PostAsync<IEnumerable<DevolucionProductoServicioResponse>, ServicioCanalCuentaRequest>
                (_sarcApiSettings.Value.Lexico.ServicioDevolucionByPR, request);

            return response.Data;
        }

        public async Task<IEnumerable<DevolucionProductoServicioResponse>> GetCobServicioCanalCuentaByProductoIdServivioIdAsync(ServicioCanalCuentaRequest request)
        {
            var response = await _sarcApiConnector.PostAsync<IEnumerable<DevolucionProductoServicioResponse>, ServicioCanalCuentaRequest>
                (_sarcApiSettings.Value.Lexico.ServicioCobroByPR, request);

            return response.Data;
        }

        public async Task<ServiceResponse<TipoCambio>> GetTipoCambioAsync()
        {
            var response = await _sarcApiConnector.PostV2Async<TipoCambio, string>
                (_sarcApiSettings.Value.SmartLink.GetTipoCambio, string.Empty);

            return response;
        }

        public async Task<ServiceResponse<int>> GetLimiteDevolucionAsync(string valor, string lexico = "")
        {
            var request = new ParametroLexicoRequest
            {
                Lexico = lexico,
                Valor = valor
            };
            var response = await _sarcApiConnector.PostV2Async<int, ParametroLexicoRequest>
                (_sarcApiSettings.Value.Lexico.LimiteDevolucion, request);

            return response;
        }

        public async Task<IEnumerable<ParametrosError>> GetSarcErrors()
        {
            var response = await _sarcApiConnector.PostAsync<IEnumerable<ParametrosError>, string>
                (_sarcApiSettings.Value.Lexico.TipoError, string.Empty);

            return response.Data;
        }

    }
}
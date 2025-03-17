using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using BCP.Framework.Logs;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sarc.WebApp.Services
{
    public class CaseService : ICaseService
    {
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;
        private readonly SarcApiConnector _sarcApiConnector;
        public CaseService(
            IOptions<SarcApiSettings> sarcApiSettings,
            SarcApiConnector sarcApiConnector
        )
        {
            _sarcApiSettings = sarcApiSettings;
            _sarcApiConnector = sarcApiConnector;
        }

        public async Task<ServiceResponse<CasoExpressResponse>> AddCasoExpressAsync(CasoExpressRequest casoExpressRequest)
        {
            var response = await _sarcApiConnector.PostAsync<CasoExpressResponse, CasoExpressRequest>(
                _sarcApiSettings.Value.Casos.RegistrarCasoExpress, casoExpressRequest);

            return response;
        }

        public async Task<ServiceResponse<CasoExpressResponse>> AddCasoAsync(CreateCasoDTO casoRequest)
        {
            var response = await _sarcApiConnector.PostAsync<CasoExpressResponse, CreateCasoDTO>(
                _sarcApiSettings.Value.Casos.RegistrarCaso, casoRequest);

            return response;
        }

        public async Task<ServiceResponse<bool?>> CerrarCasoAsync(CerrarCasoRequest casoRequest)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, CerrarCasoRequest>(
                _sarcApiSettings.Value.Casos.CerrarCaso, casoRequest);

            return response;
        }

        public async Task<ServiceResponse<bool?>> UpdateCasoOrigenAsync(UpdateOrigenCasoRequest request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateOrigenCasoRequest>(
                _sarcApiSettings.Value.Casos.ActualizarCasoOrigen, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> UpdateSolucionInfoAdicionalAsync(UpdateSolucionCasoInfoAdicionalRequest request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateSolucionCasoInfoAdicionalRequest>(
                _sarcApiSettings.Value.Casos.ActualizarAsignacionInfoAdicional, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> UpdateSolucionCasoAsync(UpdateCasoSolucionRequest request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateCasoSolucionRequest>(
                _sarcApiSettings.Value.Casos.ActualizarCasoSolucion, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> UpdateInfoRespuestaAsync(UpdateCasoViaEnvio request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateCasoViaEnvio>(
                _sarcApiSettings.Value.Casos.ActualizaInfoRespuesta, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> UpdateDevolucionCobroAsync(UpdateDevolucionCobroRequest request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateDevolucionCobroRequest>(
                _sarcApiSettings.Value.Casos.ActualizarCasoDevolucionCobro, request);

            return response;
        }

        public async Task<ServiceResponse<bool?>> RechazarCasoSolucionadoAsync(UpdateCasoRechazarDTO request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateCasoRechazarDTO>(
                _sarcApiSettings.Value.Casos.RechazarCasoSolucionado, request);

            return response;
        }
        public async Task<ServiceResponse<bool?>> RechazarCasoAsignadoAsync(UpdateCasoRechazoAnalistaDTO request)
        {
            var response = await _sarcApiConnector.PostV2Async<bool?, UpdateCasoRechazoAnalistaDTO>(
                _sarcApiSettings.Value.Casos.RechazarCasoAsignado, request);

            return response;
        }

        public async Task<ServiceResponse<List<CasoAnalista>>> GetCasosByAnalistaAsync( string matricula, string estado)
        {
            var casosAnalistaRequest = new GetCasoAnalistaRequest
            {
                Estado = estado,
                Usuario = matricula
            };
            var response = await _sarcApiConnector.PostV2Async<List<CasoAnalista>, GetCasoAnalistaRequest>
                (_sarcApiSettings.Value.Casos.ObtenerCasoAllByAnalista, casosAnalistaRequest);

            return response;
        }

        public async Task<ServiceResponse<List<CasosByEstadoResponse>>> GetCasosByEstadoAsync(string estado)
        {
            var casosAnalistaRequest = new GetCasoAnalistaRequest
            {
                Estado = estado
            };

            var response = await _sarcApiConnector.PostV2Async<List<CasosByEstadoResponse>, GetCasoAnalistaRequest>
                (_sarcApiSettings.Value.Casos.ObtenerCasosByEstado, casosAnalistaRequest);

            return response;
        }

        public async Task<ServiceResponse<CasoDTOAll>> GetCasoByNroCasoAsync(string nroCaso)
        {
            var request = new GetCasoByNroCasoRequest { NroCaso = nroCaso };
            var response = await _sarcApiConnector.PostV2Async<CasoDTOAll, GetCasoByNroCasoRequest>
                (_sarcApiSettings.Value.Casos.ObtenerCasoByNroCaso, request);

            return response;
        }

        public async Task<ServiceResponse<CasoAll>> GetCasoAllAsync(string nroCaso)
        {
            var request = new GetCasoByNroCasoRequest { NroCaso = nroCaso };
            var response = await _sarcApiConnector.PostV2Async<CasoAll, GetCasoByNroCasoRequest>
                (_sarcApiSettings.Value.Casos.ObtenerCasoAll, request);

            return response;
        }
    }
}
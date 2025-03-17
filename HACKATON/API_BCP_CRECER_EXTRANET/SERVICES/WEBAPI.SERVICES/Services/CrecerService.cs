using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Crecer;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using BCP.CROSS.MODELS.DTOs.Crecer;
using Microsoft.Extensions.Options;
using BCP.CROSS.SECRYPT;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http;
using System.Net.Mime;
using BCP.Framework.Logs;

namespace WEBAPI.SERVICES.Services
{
    public class CrecerService : ICrecerService
    {
        private const string ContentType = "application/json";
        private readonly ICrecerRepository _crecerRepository;
        private readonly ICrecerService _crecerService;
        private readonly CRECERAPI.Crecer _crecerSettings;
        private readonly HttpClient _httpClient;
        private readonly IManagerSecrypt _secrypt;
        private readonly ILoggerManager _logger;
        public CrecerService(IHttpClientFactory httpClientFactory, IOptions<CRECERAPI.Crecer> crecerSettings, IManagerSecrypt secrypt, ICrecerService crecerService)
        {
            this._crecerService = crecerService;
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this._httpClient = httpclientFactory.CreateClient("ApiBCPCrecerIntra");
            this._crecerSettings = crecerSettings.Value;
            this._secrypt = secrypt;
        }
        private async Task<EntityResponse> PostAsync<EntityResponse, EntityRequest>(Uri url, EntityRequest request)
          where EntityResponse : class
        {
            EntityResponse response;
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"BCP_CRECER_API {DateTime.Now:yyyyMMddhhmmssff}");
            using var jsonRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            if (!postResponse.IsSuccessStatusCode)
            {
                _logger.Fatal($"{postResponse.ReasonPhrase} {postResponse.RequestMessage.RequestUri.ToString()}");
                throw new Exception($"Code: 700 - {postResponse.ReasonPhrase} - Revice el servicio de Consultas Categorías");
            }
            response = await postResponse.Content.ReadFromJsonAsync<EntityResponse>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponse<List<GetCategoriasResponse>>> GetCategoriasAsync(string responseId)
        {
            List<CategoriasResponse> CategoriasResponse = new();
            var uri = new Uri($"{_crecerSettings.BaseUrl}{_crecerSettings.Categorias}");           
            var response = await PostAsync<GetCategoriasResponse, string> (uri, responseId);

            if (response != null)
            {
                CategoriasResponse = new List<CategoriasResponse>();
                foreach (var item in response.Categorias)
                {
                    var categorias = new CategoriasResponse
                    {
                        Idc = $"{item.IdcNumero}{item.IdcTipo}{item.IdcExtension}",
                        NroIdc = item.IdcNumero,
                        IdcTipo = item.IdcTipo,
                        IdcExtension = item.IdcExtension,
                        Nombres = item.Nombres,
                        NombreCompleto = $"{item.Paterno} {item.Materno} {item.Nombres}",
                        PaternoRazonSocial = item.Paterno,
                        Materno = item.Materno,
                        Direccion = item.Direccion,
                        Direccion1 = string.Empty,
                        Direccion2 = string.Empty,
                        Telefono1 = item.Telefono,
                        Cleular1 = item.Celular,
                        Email = string.Empty,
                        TipoCliente = "N"

                    };
                    CategoriasResponse.Add(categorias);
                }

            }
            return new GetClientesResponseIntermedio
            {
                Clientes = clienteDBCResponse,
                codigo = response.Codigo,
                Exito = response.Exito,
                Mensaje = response.Mensaje,
                Operacion = response.Operation
            };
        }
        public async Task<ServiceResponse<List<ObtieneEmpresasByCategoriaCiudadResponse>>> ObtieneEmpresasByCategoriaCiudadAsync(ObtieneEmpresasByCategoriaCiudadRequest CuentasRequest, string requestId)
        {

            var responseCobros = await _crecerRepository.ObtieneEmpresasByCategoriaCiudadAsync(CuentasRequest);
            var response = new ServiceResponse<List<ObtieneEmpresasByCategoriaCiudadResponse>>()
            {
                Data = responseCobros,
                Meta = {
                    Msj = responseCobros != null ? "Success" : "Cobros no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCobros != null ? 200 : 404
                }
            };

            return response;
        }
    }
}

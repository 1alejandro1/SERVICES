using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using BCP.CROSS.MODELS.Infocliente;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTEJURIDICO.Services
{
    public class InfoclientePjServices: IInfoclientePjServices
    {
        private const string ContentType = "application/json";
        private readonly InfoclientePj _infoclienteSettings;
        private readonly HttpClient _httpClient;
        private readonly ILoggerManager _logger;
        private readonly IManagerSecrypt _secrypt;
        public InfoclientePjServices(IHttpClientFactory httpClientFactory, IOptions<InfoclientePj> infoclienteSettings, IManagerSecrypt secrypt)
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this._httpClient = httpclientFactory.CreateClient("API_INFOCLIENTE_JURIDICO");
            this._infoclienteSettings = infoclienteSettings.Value;
            this._secrypt = secrypt;
        }

        private async Task<EntityResponse> PostAsync<EntityResponse, EntityRequest>(Uri url, EntityRequest request)
           where EntityResponse : class
        {
            EntityResponse response;
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"SARC_API {DateTime.Now:yyyyMMddhhmmssff}");
            using var jsonRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            if (!postResponse.IsSuccessStatusCode)
            {
                _logger.Fatal($"{postResponse.ReasonPhrase} {postResponse.RequestMessage.RequestUri.ToString()}");
                throw new Exception($"Code: 700 - {postResponse.ReasonPhrase} - Revice el servicio de Consultas Clientes");
            }
            response = await postResponse.Content.ReadFromJsonAsync<EntityResponse>().ConfigureAwait(false);

            return response;
        }

         public async Task<GetClientesResponseIntermedio> GetClientePjByIdcAsync(GetClientByIdcRequest request, string matricula, string requestId)
         {
             List<GetClienteResponse> clientesResponse = new();

             var requestJuridico = new ClientePjByIdcRequest
             {
                 idcNumero = request.Idc?.Trim().PadLeft(8, '0'),
                 idcTipo = request.TipoIdc?.Trim().ToUpper(),
                 idcExtension = request.Extension?.Trim().ToUpper(),
                 canal = _infoclienteSettings.Canal,
                 matricula = matricula,
                 operacion = requestId
             };

             var uri = new Uri($"{_infoclienteSettings.BaseUrl}{_infoclienteSettings.ConsultaClienteIdc}");
             var response = await PostAsync<ClientePjByIdcResponse, ClientePjByIdcRequest>
                 (uri, requestJuridico);


             if (response != null && response.Exito && response.Data.Count > 0)
             {
                 clientesResponse = new List<GetClienteResponse>();
                 foreach (var item in response.Data)
                 {
                     var cliente = new GetClienteResponse
                     {
                         Idc = item.cjIdcCompleto,
                         NroIdc = item.cjNroNit,
                         IdcTipo = item.cjTipoNit,
                         IdcExtension = item.cjExtNit,
                         PaternoRazonSocial = item.cjRazonSocial,
                         Nombres = string.Empty,
                         Materno = string.Empty,
                         Direccion = string.Empty,
                         Direccion1 = string.Empty,
                         Direccion2 = string.Empty,
                         Telefono1 = string.Empty,
                         Cleular1 = string.Empty,
                         Email = string.Empty,
                         TipoCliente = "J"
                     };
                     clientesResponse.Add(cliente);
                 }

             }
             return  new GetClientesResponseIntermedio 
             {
                 Clientes= clientesResponse,
                 codigo=response.Codigo,
                 Exito=response.Exito,
                 Mensaje=response.Mensaje,
                 Operacion=requestId
             };
         }

        public async Task<GetClientesResponseIntermedio> GetClientePjByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            List<GetClienteResponse> clienteDBCResponse = new();
            var requestPn = new ClientePjByDbcRequest
            {
                nit = "",
                razonSocial = request.PaternoRazonSocial.Trim().ToUpper()
            };

            var uri = new Uri($"{_infoclienteSettings.BaseUrl}{_infoclienteSettings.ConsultaClienteDbc}");
            var response = await PostAsync<ClientePjByDbcResponse, ClientePjByDbcRequest>(uri, requestPn);

            if (response != null && response.Exito)
            {
                clienteDBCResponse = new List<GetClienteResponse>();
                foreach (var item in response.data)
                {
                    var cliente = new GetClienteResponse
                    {
                        Idc = $"{item.nit}{item.tipoNit}{item.extensionNit}",
                        NroIdc = item.nit,
                        IdcTipo = item.tipoNit,
                        IdcExtension = item.extensionNit,
                        Nombres = item.nombreComercial,
                        PaternoRazonSocial = item.razonSocial,
                        Materno = string.Empty,
                        Direccion = item.direccion,
                        Direccion1 = string.Empty,
                        Direccion2 = string.Empty,
                        Telefono1 = item.telefono,
                        Cleular1 = item.telefono,
                        Email = string.Empty,
                        TipoCliente = "J"

                    };
                    clienteDBCResponse.Add(cliente);
                }

            }
            return new GetClientesResponseIntermedio
            {
                Clientes = clienteDBCResponse,
                codigo = response.Codigo,
                Exito = response.Exito,
                Mensaje = response.Mensaje,
                Operacion = requestId
            };
        }

        public bool ValidarIdc(string tipoIdc)
        {
            return _infoclienteSettings.TipoIdc.Contains(tipoIdc);
        }
    }
}

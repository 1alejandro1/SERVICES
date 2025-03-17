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

namespace BCP.CROSS.INFOCLIENTE.Services
{
    public class InfoclientePnServices: IInfoclientePnServices
    {
        private const string ContentType = "application/json";
        private readonly InfoclientePn _infoclienteSettings;
        private readonly HttpClient _httpClient;
        private readonly ILoggerManager _logger;
        private readonly IManagerSecrypt _secrypt;
        public InfoclientePnServices(IHttpClientFactory httpClientFactory, IOptions<InfoclientePn> infoclienteSettings, IManagerSecrypt secrypt)
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this._httpClient = httpclientFactory.CreateClient("API_INFOCLIENTE");
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

        public bool ValidarIdc(string tipoIdc)
        {
            return _infoclienteSettings.TipoIdc.Contains(tipoIdc);
        }

        public async Task<GetClienteResponseIntermedio> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId)
        {
            GetClienteResponseIntermedio clienteResponse = new GetClienteResponseIntermedio();
            var requestPn = new ClientePnByIdcRequest
            {
                Canal = _infoclienteSettings.Canal,
                Usuario = _infoclienteSettings.Usuario,
                Password = _secrypt.Desencriptar(_infoclienteSettings.Password),
                Idc = request.Idc?.Trim().PadLeft(8, '0'),
                TipoIdc = request.TipoIdc.Trim().ToUpper(),
                ExtensionIdc = request.Extension.Trim().ToUpper(),
                Complemento = "00",
                OperacionOrigen = requestId
            };

            var uri = new Uri($"{_infoclienteSettings.BaseUrl}{_infoclienteSettings.ConsultaClienteIdc}");
            var response = await PostAsync<ClientePnByIdcResponse, ClientePnByIdcRequest>(uri, requestPn);
            clienteResponse.exito = response.exito;
            clienteResponse.mensaje=response.mensaje;
            clienteResponse.operacion = response.operacion;
            clienteResponse.codigo = response.codigo;
            if (response != null && response.exito)
            {
                var cliente = new GetClienteResponse
                {
                    Idc = response.InfoCliente.idc,
                    NroIdc = requestPn.Idc,
                    IdcTipo = request.TipoIdc,
                    IdcExtension = request.Extension,
                    Nombres = response.InfoCliente.nombre,
                    NombreCompleto = $"{response.InfoCliente.paterno} {response.InfoCliente.materno} {response.InfoCliente.nombre}",
                    PaternoRazonSocial = response.InfoCliente.paterno,
                    Materno = response.InfoCliente.materno,
                    Direccion = response.InfoDireccion.Where(x => x.tipoDireccionId.Equals(19)).Select(d => d.direccion).FirstOrDefault(),
                    Direccion1 = response.InfoDireccion.Where(x => x.tipoDireccionId.Equals(20)).Select(d => d.direccion).FirstOrDefault(),
                    Direccion2 = response.InfoDireccion.Where(x => x.tipoDireccionId.Equals(21)).Select(d => d.direccion).FirstOrDefault(),
                    Telefono1 = response.InfoDatoPersonal?.telefono,
                    Cleular1 = response.InfoDatoPersonal?.celular,
                    Email = response.InfoDatoPersonal.correoElectronico,
                    TipoCliente = "N"
                };

                clienteResponse.cliente = cliente;
            }

            return clienteResponse;
        }

        public async Task<GetClientesResponseIntermedio> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            List<GetClienteResponse> clienteDBCResponse = new();
            var requestPn = new ClientePnByDbcRequest
            {
                Operation = request.Nombres.Trim().ToUpper(),
                Data = new ClientePn
                {
                    Paterno = request.PaternoRazonSocial.Trim().ToUpper(),
                    Materno = request.Materno.Trim().ToUpper(),
                    Nombres = request.Nombres.Trim().ToUpper()
                }
            };

            var uri = new Uri($"{_infoclienteSettings.BaseUrl}{_infoclienteSettings.ConsultaClienteDbc}");
            var response = await PostAsync<ClientePnByDbcResponse, ClientePnByDbcRequest>(uri, requestPn);

            if (response != null && response.Exito)
            {
                clienteDBCResponse = new List<GetClienteResponse>();
                foreach (var item in response.Data)
                {
                    var cliente = new GetClienteResponse
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
                    clienteDBCResponse.Add(cliente);
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
    }
}

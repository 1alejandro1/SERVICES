using BCP.CROSS.MODELS.Swamp;
using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BCP.CROSS.SWAMP.Services
{
    internal class SwampServices:ISwampServices
    {
        private readonly SwampSettings _swampSettings;
        private readonly HttpClient _httpClient;
        private readonly IManagerSecrypt _managerSecrypt;
        public SwampServices(IHttpClientFactory httpClientFactory, IManagerSecrypt managerSecrypt, IOptions<SwampSettings> swampSettings)
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this._managerSecrypt=managerSecrypt;
            this._httpClient = httpclientFactory.CreateClient("API_SWAMPCORE");
            this._swampSettings = swampSettings.Value;
        }

        private async Task<T> ConsultaApi<T>(object request,string metodo)
        {
            string token = this._managerSecrypt.Desencriptar(_swampSettings.PublicToken);
            string password = this._managerSecrypt.Desencriptar(_swampSettings.Password);
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_swampSettings.User + ":" + password)));
            this._httpClient.DefaultRequestHeaders.Add("CorrelationId", DateTime.Now.ToString("ddMMyyyyHHmmss"));
            this._httpClient.DefaultRequestHeaders.Add("Channel", "SARC");
            this._httpClient.DefaultRequestHeaders.Add("PublicToken", token);
            this._httpClient.DefaultRequestHeaders.Add("AppUserId", _swampSettings.AppUserId);
            this._httpClient.DefaultRequestHeaders.Add("api-version", "1.0");

            using var apiRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var uri = new Uri($"{this._swampSettings.UriSwamp}{metodo}");
            var apiResponse = await this._httpClient.PostAsync(uri, apiRequest);

            if (apiResponse.IsSuccessStatusCode)
            {
                //await this.ThrowServiceToServiceErrors(apiResponse).ConfigureAwait(false);
                var response = await apiResponse.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
                return response;
            }
            return default(T);
        }

        public async Task<GetDatosBasicosTicketServicesResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request)
        {
            return await ConsultaApi<GetDatosBasicosTicketServicesResponse>(request, this._swampSettings.MethodDatosBasicosClienteTicket);            
        }

        public async Task<InsertEventSwampResponse> InsertEventoSwampAsync(InsertEventoRequest request)
        {
            return await ConsultaApi<InsertEventSwampResponse>(request, this._swampSettings.MethodInsertEvento);
        }

        public async Task<TipoCambioSwampResponse> TipoCambioResponse(bool request=false)
        {
            return await ConsultaApi<TipoCambioSwampResponse>(request, this._swampSettings.MethodInsertEvento);
        }
    }
}

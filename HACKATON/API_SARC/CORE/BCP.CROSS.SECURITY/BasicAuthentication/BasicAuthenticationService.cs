using BCP.CROSS.COMMON;
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

namespace BCP.CROSS.SECURITY.BasicAuthentication
{
    public interface IBasicAuthenticationService
    {
        Task<AuthorizationResponse> Validate(AuthorizationRequest request);
    }
    public class BasicAuthenticationService : IBasicAuthenticationService
    {
        private readonly AuthSettings _authSettings;
        private readonly HttpClient httpClient;

        public BasicAuthenticationService(IHttpClientFactory httpClientFactory,IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this.httpClient = httpclientFactory.CreateClient("AUTHORIZE");
        }
        public async Task<AuthorizationResponse> Validate(AuthorizationRequest request)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(request.Username + ":" + request.Password)));
                httpClient.DefaultRequestHeaders.Add("Correlation-Id", DateTime.Now.ToString("yyyyyddmmhhss"));
                httpClient.DefaultRequestHeaders.Add("api-version", "1.0");

                using var authrequest = new StringContent(JsonSerializer.Serialize(request.ChanelAuthorized), Encoding.UTF8, "application/json");
                var uri = new Uri($"{_authSettings.UriBasicAuthorization}{_authSettings.MethodBasicAuthorization}");
                var response = await this.httpClient.PostAsync(uri, authrequest);

                if (!response.IsSuccessStatusCode)
                {
                    await this.ThrowServiceToServiceErrors(response).ConfigureAwait(false);
                }

                var responseValidate = await response.Content.ReadFromJsonAsync<AuthorizationResponse>().ConfigureAwait(false);
                return responseValidate;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse.Meta.Msj);
        }
    }
}

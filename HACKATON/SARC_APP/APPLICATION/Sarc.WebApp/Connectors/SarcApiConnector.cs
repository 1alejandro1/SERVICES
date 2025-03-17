using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sarc.WebApp.Connectors
{
    public class SarcApiConnector
    {
        private const string ContentType = "application/json";
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private HttpClient _httpClient;
        private readonly string Password;
        private readonly string PublicToken;
        public SarcApiConnector(
            ILoggerManager logger,
            IHttpClientFactory httpClientFactory,
            IOptions<SecryptOptions> securitySettigns,
            IManagerSecrypt secrypt
        )
        {
            _logger = logger;
            _securitySettigns = securitySettigns;
            _secrypt = secrypt;
            IHttpClientFactory _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SARC_API");
            Password = _secrypt.Desencriptar(_securitySettigns.Value.Password);
            PublicToken = _secrypt.Desencriptar(_securitySettigns.Value.PublicToken);
        }

        public async Task<ServiceResponse<EntityResponse>> PostAsync<EntityResponse, EntityRequest>(string url, EntityRequest request)
            where EntityResponse : class
        {
            ServiceResponse<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("CorrelationId", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError)
            {
                await ThrowServiceToServiceErrors(postResponse).ConfigureAwait(false);
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{JsonSerializer.Serialize(postResponse)}");
                response.Meta = new Meta { Msj = postResponse.ReasonPhrase, StatusCode = (int)postResponse.StatusCode };
                return response;
            }
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse>>().ConfigureAwait(false);

            return response;
        }

        public async Task<ServiceResponse<EntityResponse>> PostV2Async<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponse<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("CorrelationId", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse?>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{JsonSerializer.Serialize(postResponse)}");
                response.Meta = new Meta { Msj = postResponse.ReasonPhrase, StatusCode = (int)postResponse.StatusCode };
                return response;
            }
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse?>>().ConfigureAwait(false);

            return response;
        }

        private static async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse?.Meta?.Msj);
        }
    }
}

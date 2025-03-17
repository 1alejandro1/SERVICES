using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Category;
using BCP.CROSS.MODELS.Generated;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Card_App.Conectors
{
    public class CardApiConnector
    {
        private const string ContentType = "application/json";
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private HttpClient _httpClient;
        private readonly string Password;
        private readonly string PublicToken;
        public CardApiConnector(
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
            _httpClient = _httpClientFactory.CreateClient("CARD_API");
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

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError)
            {
                await ThrowServiceToServiceErrors(postResponse).ConfigureAwait(false);
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response.Meta = new Meta { Msj = postResponse.ReasonPhrase, StatusCode = (int)postResponse.StatusCode };

                return response;
            }
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse>>().ConfigureAwait(false);

            return response;
        }

        public async Task<GeneratedResponse> PostGeneratedAsync<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            GeneratedResponse response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);
                        

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<GeneratedResponse>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response.message = "Fatal Error";
                return response;
            }
            //var resultContent = await postResponse.Content.ReadAsStringAsync();
            //response = JsonConvert.DeserializeObject<GeneratedResponse>(resultContent);
            response = await postResponse.Content.ReadFromJsonAsync<GeneratedResponse>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponse<EntityResponse>> PostV2Async<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponse<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);


            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse?>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response.Meta = new Meta { Msj = postResponse.ReasonPhrase, StatusCode = (int)postResponse.StatusCode };
                return response;
            }
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<EntityResponse?>>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponseV2<ListCategoryResponse>> PostV3Async<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponseV2<ListCategoryResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
               "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            _logger.Information(System.Text.Json.JsonSerializer.Serialize(postResponse));
            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<ListCategoryResponse>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");              
                return response;
            }
            //var resultContent = await postResponse.Content.ReadAsStringAsync();
            //response = JsonConvert.DeserializeObject<ListCategoryResponse>(resultContent);
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<ListCategoryResponse>>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponseV2<EntityResponse>> PostCommerceAsync<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponseV2<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
               "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            _logger.Information(System.Text.Json.JsonSerializer.Serialize(postResponse));
            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse?>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                return response;
            }
            //var resultContent = await postResponse.Content.ReadAsStringAsync();
            //response = JsonConvert.DeserializeObject<ListCategoryResponse>(resultContent);
            response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse?>>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponseV2<EntityResponse>> PostServiceAsync<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponseV2<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
               "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            _logger.Information(System.Text.Json.JsonSerializer.Serialize(postResponse));
            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                return response;
            }
            var resultContent = await postResponse.Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<ServiceResponseV2<EntityResponse>>(resultContent);
            //response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse>>().ConfigureAwait(false);

            return response;
        }
        public async Task<ServiceResponseV2<EntityResponse>> PostLoginAsync<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ServiceResponseV2<EntityResponse> response = new();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
               "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_securitySettigns.Value.User + ":" + Password)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_securitySettigns.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _securitySettigns.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("PublicToken", PublicToken);
            _httpClient.DefaultRequestHeaders.Add("AppUserId", _securitySettigns.Value.AppUserId);

            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(url, jsonRequest).ConfigureAwait(false);
            _logger.Information(System.Text.Json.JsonSerializer.Serialize(postResponse));
            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse>>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {
                _logger.Fatal($"{System.Text.Json.JsonSerializer.Serialize(postResponse)}");
                return response;
            }
            var resultContent = await postResponse.Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<ServiceResponseV2<EntityResponse>>(resultContent);
            //response = await postResponse.Content.ReadFromJsonAsync<ServiceResponseV2<EntityResponse>>().ConfigureAwait(false);

            return response;
        }
        private static async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse?.Meta?.Msj);
        }
    }
}

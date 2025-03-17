using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Segurinet;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Card_App.Conectors
{
    public class AuthConnector
    {
        private const string ContentType = "application/json";
        private readonly ILoggerManager _logger;
        private readonly IOptions<SecryptOptions> _securitySettigns;
        private readonly IManagerSecrypt _secrypt;
        private HttpClient _httpClient;
        private readonly string Password;
        private readonly string PublicToken;
        private readonly IOptions<SegurinetApiSettings> _segurinetApiSettings;

        public IManagerSecrypt Secrypt => _secrypt;

        public AuthConnector(
           ILoggerManager logger,
           IHttpClientFactory httpClientFactory,
           IOptions<SecryptOptions> securitySettigns,
           IOptions<SegurinetApiSettings> segurinetApiSettings,
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
            _segurinetApiSettings = segurinetApiSettings;

            Password = _secrypt.Desencriptar(_segurinetApiSettings.Value.Password);
            PublicToken = _secrypt.Desencriptar(_segurinetApiSettings.Value.PublicToken);
        }

        public async Task<ApiSegurinetResponse> LoginAsyncV2<EntityResponse, EntityRequest>(string url, EntityRequest request)
        {
            ApiSegurinetResponse response = new();
            var baseUrl = _segurinetApiSettings.Value.BaseUrl;            

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_segurinetApiSettings.Value.User + ":" + Password)));
            //_httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{_segurinetApiSettings.Value.Channel} {DateTime.Now:yyyyMMddmmssff}");
            _httpClient.DefaultRequestHeaders.Add("Channel", _segurinetApiSettings.Value.Channel);
            _httpClient.DefaultRequestHeaders.Add("Token", PublicToken);
            //_httpClient.DefaultRequestHeaders.Add("AppUserId", _segurinetApiSettings.Value.AppUserId);


            using var jsonRequest = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, ContentType);
            var postResponse = await _httpClient.PostAsync(baseUrl+url, jsonRequest).ConfigureAwait(false);

            if ((int)postResponse.StatusCode == StatusCodes.Status500InternalServerError && postResponse.Content.Headers.ContentLength > 0)
            {

                response = await postResponse.Content.ReadFromJsonAsync<ApiSegurinetResponse>().ConfigureAwait(false);
                return response;
            }
            if (!postResponse.IsSuccessStatusCode && postResponse.Content.Headers.ContentLength <= 0)
            {

                response.message = "Fatal Error";
                return response;
            }
            //var resultContent = await postResponse.Content.ReadAsStringAsync();
            //response = JsonConvert.DeserializeObject<GeneratedResponse>(resultContent);
            response = await postResponse.Content.ReadFromJsonAsync<ApiSegurinetResponse>().ConfigureAwait(false);

            return response;
        }


    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Enums;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.DTOs.PushNotification;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
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
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IManagerSecrypt _secrypt;
        private HttpClient _httpClient;

        public NotificationService(ILoggerManager logger,  IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> authSettings, IManagerSecrypt secrypt)
        {
            _logger = logger;
            _appSettings = authSettings;
            _secrypt = secrypt;
            IHttpClientFactory httpclientFactory = httpClientFactory;
            _httpClient = httpclientFactory.CreateClient("PUSH_NOTIFICATION");
        }

        public async Task<ServiceResponse<string>> SendNotificationAsync(List<Client> client, int viaEnvioCodigo, string requestId, InsertCasoResponse casoResponse)
        {
            string pass = _secrypt.Desencriptar(_appSettings.Value.ApiPushNotificationSettings.Password);
            string channel = _appSettings.Value.ApiPushNotificationSettings.Channel;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_appSettings.Value.ApiPushNotificationSettings.User + ":" + pass)));
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", $"{channel}{requestId}");

            #region Build Request PushNotification
                Target target = viaEnvioCodigo switch
                {
                    (int)NotifcationChannels.Email => _ = new Target { Email = true },
                    (int)NotifcationChannels.SMS => _ = new Target { Sms = true },
                    _ => throw new System.Exception("Canal de notificacion no configurado")
                };

                client[0].Email[0] += $"; {_appSettings.Value.ApiPushNotificationSettings.CC}";
                var pushNotificationRequest = new PushNotificationRequest
                {
                    Target = target,
                    Clients = client,
                    Application = channel,
                    PublicToken = _secrypt.Desencriptar(_appSettings.Value.ApiPushNotificationSettings.PublicToken),
                    AppUserId = _appSettings.Value.ApiPushNotificationSettings.AppUserId, 
                    Title = _appSettings.Value.ApiPushNotificationSettings.Title,
                    Message = $"{_appSettings.Value.ApiPushNotificationSettings.Message} {casoResponse.NroCarta}",
                    Data = new { EmailFrom = _appSettings.Value.ApiPushNotificationSettings.From }
                };
            #endregion


            var stringRequest = JsonSerializer.Serialize(pushNotificationRequest);
            using var jsonRequest = new StringContent( stringRequest, Encoding.UTF8, "application/json");

            _logger.Information($"AppUserId: {pushNotificationRequest.AppUserId} - {requestId}, Request PushNotification: {stringRequest}");

            var uri = new Uri($"{_appSettings.Value.ApiPushNotificationSettings.PushEndpoint}");
            var response = await _httpClient.PostAsync(uri, jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Information($"AppUserId: {pushNotificationRequest.AppUserId} - {requestId}, Response PushNotification: {JsonSerializer.Serialize(response)}");
                return new ServiceResponse<string>
                {
                    Data = $"No se pudo enviar el codigo de caso al cliente",
                    Meta = new Meta { Msj = response.ReasonPhrase, StatusCode = (int)response.StatusCode, ResponseId = requestId }
                };
            }

            var responseNotification = await response.Content.ReadFromJsonAsync<PushNotificationResponse>().ConfigureAwait(false);

            if (responseNotification.State == "00")
            {
                    Meta meta = viaEnvioCodigo switch
                    {
                        (int)NotifcationChannels.Email => meta = new Meta {
                            Msj = responseNotification.Data.Email.State == "00" ? $"Correo enviado a: {client[0].Email[0]}" : responseNotification.Data.Email.Message,
                            StatusCode = int.Parse(responseNotification.Data.Email.State),
                            ResponseId = requestId },

                        (int)NotifcationChannels.SMS => meta = new Meta { 
                            Msj = responseNotification.Data.Sms.State == "00" ? $"Mensaje enviado a: {client[0].PhoneNumber}" : responseNotification.Data.Sms.Message,
                            StatusCode = int.Parse(responseNotification.Data.Sms.State),
                            ResponseId = requestId },

                        _ => throw new System.Exception("Canal de notificacion no configurado")
                    };

                return new ServiceResponse<string> {
                    Data = "Success",
                    Meta = meta
                };

            }

            return new ServiceResponse<string> { 
                Data = $"No se pudo enviar el correo a {client[0].Email[0]}",
                Meta = new Meta { Msj = responseNotification.Message, StatusCode = int.Parse(responseNotification.State), ResponseId = requestId }
            };
        }

        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse.Meta.Msj);
        }
    }
}

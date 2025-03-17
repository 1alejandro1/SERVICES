using BCP.CROSS.MODELS;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET.Services
{
    public class SegurinetServices: ISegurinetServices
    {
        private readonly SegurinetSettings _segurinetSettings;
        private readonly HttpClient _httpClient;
        public SegurinetServices(IHttpClientFactory httpClientFactory, IOptions<SegurinetSettings> segurinetSettings)
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            this._httpClient = httpclientFactory.CreateClient("WCF_SEGURINET");
            this._segurinetSettings = segurinetSettings.Value;
        }


        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = new LoginResponse();
            //this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(request.Username + ":" + request.Password)));
            //this._httpClient.DefaultRequestHeaders.Add("Correlation-Id", DateTime.UtcNow.ToString());
            this._httpClient.DefaultRequestHeaders.Add("api-version", "1.0");
            LoginServiceRequest serviceRequest = new LoginServiceRequest
            {
                usuarioSegurinet = request.usuario,
                passwordSegurinet = request.password
            };
            using var apiRequest = new StringContent(JsonSerializer.Serialize(serviceRequest), Encoding.UTF8, "application/json");
            var uri = new Uri($"{this._segurinetSettings.UriSegurinet2}{this._segurinetSettings.MethodLogin}");
            var apiResponse = await this._httpClient.PostAsync(uri, apiRequest);

            if (apiResponse.IsSuccessStatusCode)
            {
                //await this.ThrowServiceToServiceErrors(apiResponse).ConfigureAwait(false);
                var result = await apiResponse.Content.ReadFromJsonAsync<LoginServiceResponse>().ConfigureAwait(false);
                if (result.CodigoSegurinet==0L)
                {
                    response.Matricula = result.CodigoSegurinet.ToString().PadLeft(2,'0') + result.InformacionLogin.Login;
                    response.Nombre = result.InformacionLogin.NombreUsuario;
                    response.Politicas = result.InformacionLogin.ItemsPoliticas;
                    response.Token = new UserToken();
                    response.Token.token = result.InformacionLogin.Roles;
                }
                else
                {
                    response.Matricula = result.CodigoSegurinet.ToString().PadLeft(2, '0');
                    response.Nombre = result.MensajeValidacion;
                }
            }
            else
            {
                response.Matricula = "99";
                response.Nombre = "OCURRIO UN ERROR AL REALIZAR LA CONSULTA SEGURINET";
            }
            return response;
        }


        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var response = new ChangePasswordResponse();
            //this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(request.Username + ":" + request.Password)));
            //this._httpClient.DefaultRequestHeaders.Add("Correlation-Id", DateTime.UtcNow.ToString());
            this._httpClient.DefaultRequestHeaders.Add("api-version", "1.0");
            CambioPasswordServiceRequest serviceRequest = new CambioPasswordServiceRequest
            {
                usuarioSegurinet = request.usuario,
                passwordSegurinet = request.password,
                nuevoPasswordSegurinet=request.newPassword
            };
            using var apiRequest = new StringContent(JsonSerializer.Serialize(serviceRequest), Encoding.UTF8, "application/json");
            var uri = new Uri($"{this._segurinetSettings.UriSegurinet2}{this._segurinetSettings.MethodChangePassword}");
            var apiResponse = await this._httpClient.PostAsync(uri, apiRequest);
            if (apiResponse.IsSuccessStatusCode)
            {
                //await this.ThrowServiceToServiceErrors(apiResponse).ConfigureAwait(false);
                var result = await apiResponse.Content.ReadFromJsonAsync<CambioPasswordServiceResponse>().ConfigureAwait(false);
                response.state = result.codigoSegurinet.ToString().PadLeft(2,'0');
                response.message = result.mensajeValidacion;
            }
            else
            {
                response.state = "99";
                response.message = "OCURRIO UN PROBLEMA AL COMUNICARSE CON EL SERVICIO";
            }                
            return response;
        }
    }
}

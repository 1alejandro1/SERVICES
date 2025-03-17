using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Login;
using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models;
using System.Net;
using BCP.CROSS.COMMON.Helpers;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System;

namespace Sarc.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private const string ContentType = "application/json";
        private readonly HttpClient _httpClient;
        private readonly IManagerSecrypt _secrypt;
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;
        public AuthService(IHttpClientFactory httpClientFactory,
            IManagerSecrypt secrypt,
            IOptions<SarcApiSettings> sarcApiSettings,
            IHttpContextAccessor httpContextAccessor
        )
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            _httpClient = httpclientFactory.CreateClient("SARC_API");
            _secrypt = secrypt;
            _sarcApiSettings = sarcApiSettings;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public async Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            loginRequest.Usuario = _secrypt.Encriptar(loginRequest.Usuario);
            loginRequest.Password = _secrypt.Encriptar(loginRequest.Password);

            ServiceResponse<LoginResponse> loginResponse = new();

            using var orderRequest = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, ContentType);
            var response = await _httpClient.PostAsync(_sarcApiSettings.Value.Segurinet.Login, orderRequest).ConfigureAwait(false);

            if ((int)response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                await this.ThrowServiceToServiceErrors(response).ConfigureAwait(false);
            }
            if ((!response.IsSuccessStatusCode && response.Content.Headers.ContentLength <= 0) || (int)response.StatusCode == 503 )
            {
                loginResponse.Meta = new Meta { Msj = response.ReasonPhrase, StatusCode = (int)response.StatusCode };
                return loginResponse;
            }

            loginResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<LoginResponse>>().ConfigureAwait(false);

            return loginResponse;
        }

        public async Task<ServiceResponse<LoginResponse>> LoginAsyncTest(LoginRequest loginRequest)
        {
            var response = await Task.Run(() =>
                new ServiceResponse<LoginResponse>
                {
                    Data = new LoginResponse
                    {
                        Nombre = "Chila Casilla Wilfredo",
                        Matricula = "BC1987",
                        Politicas = new List<string> {
                    "SARC_MOD_ING",
                    "SARC_MOD_REG",
                    "SARC_MOD_CON",
                    "SARC_MOD_ALEANA",
                    "SARC_MOD_BANENTANA",
                    "SARC_MOD_BANENTREC",
                    "SARC_MOD_BANCONSARC",
                    "SARC_MOD_BANCONS",
                    "SARC_MOD_HISCASSARC",
                    "SARC_MOD_REG_EXP",
                    "SARC_MOD_SUP",
                    "SARC_MOD_BANENT",
                    "SARC_MOD_ALESUP",
                    "SARC_MOD_ASIGCAS",
                    "SARC_MOD_MODCAS",
                    "SARC_MOD_SOLCAS",
                    "SARC_MOD_REP",
                    "SARC_MOD_EXP",
                    "SARC_MOD_DEVATMPOS",
                    "SARC_MOD_REPPROSER",
                    "SARC_MOD_REPANA",
                    "SARC_MOD_REPTIPRECSER",
                    "SARC_MOD_REPCNR",
                    "SARC_MOD_REPBAS",
                    "SARC_MOD_ANACASSOL",
                    "SARC_MOD_ANAESP",
                    "SARC_MOD_ESPCAPINT",
                    "SARC_MOD_ASFI",
                    "SARC_MOD_ADM",
                    "SARC_MOD_USU",
                    "SARC_MOD_TIPCASO",
                    "SARC_MOD_PROSER",
                    "SARC_MOD_TIPCAR",
                    "SARC_MOD_TIPSOL",
                    "SARC_MOD_ESP",
                    "SARC_MOD_REACAS",
                    "SARC_MOD_TARIFARIO",
                    "SARC_MOD_TARIFARIO_E",
                    "SARC_MOD_TARIFARIO_V",
                    "SARC_MOD_DEVOL",
                    "SARC_MOD_COBRO",
                    "SARC_MOD_REP_COB_DEV",
                    "SARC_MOD_REP_COB_DEV_PR",
                    "SARC_MOD_REP_COB_DEV_SW",
                    "SARC_MOD_LIMITE"
                        },
                        Token = new UserToken
                        {
                            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJNYXRyaWN1bGEiOiJUMjUyMDgiLCJOb21icmUiOiJUb3JvIFNhbGFzIERvdWdsYXMiLCJqdGkiOiIyNmVlNjZhNC1jZmZhLTQzZDktYTZlYy00ZDNjOGFkNzE4NGMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiVXN1YXJpbyBhZG1pbmlzdHJhZG9yIiwiVXN1YXJpbyBhbmFsaXN0YSIsIlVzdWFyaW8gYW5hbGlzdGEgU29sdWNpb25lcyBEaXJlY3RhcyIsIlVzdWFyaW8gYW5hbGlzdGEgb3RybyIsIlVzdWFyaW8gYW5hbGlzdGEgcGxhdGFmb3JtYSIsIkFuYWxpc3RhIFNBUkMgLSBTVVAiLCJVc3VhcmlvIENDQUwiLCJVc3VhcmlvIFBhcmFtZXRyaXphY2lvbiBDb2Jyb3MiLCJVc3VhcmlvIENvbnN1bHRhIiwiVXN1YXJpbyBQQXJhbWV0cml6YWNpb24gRGV2b2x1Y2lvbmVzIiwiVXN1YXJpbyBDb25zdWx0YSBEZXZvbHVjaW9uZXMgQ29icm9zIiwiVXN1YXJpbyBDb25zdWx0YSBEZXZvbHVjaW9uZXMgQ29icm9zIFBSIiwiVXN1YXJpbyBDb25zdWx0YSBEZXZvbHVjaW9uZXMgQ29icm9zIFNXQU1QIiwiVXN1YXJpbyBzdXBlcnZpc29yIiwiVXN1YXJpbyBWaXN1YWxpemFjaW9uIFRhcmlmYXJpbyIsIlVzdWFyaW8gcHJ1ZWJhIl0sImV4cCI6Mjc2ODM5MDI2N30.v_pWHwU5n1Hb4kIXhddRKUrPACgaC3hqDPIsTkbce0U",
                            Expiration = Convert.ToDateTime("2057-09-01T09:17:47.575663Z")
                        }
                    },
                    Meta = new Meta
                    {
                        Msj = "OPERACION EJECUTADA CORRECTAMENTE",
                        StatusCode = 200,
                        ResponseId = "2021120110242610"
                    }
                }
            );
            return response;
        }

        public async Task<LoginResponse> GetToken()
        {
            return await Task.Run(() => _session.Get<LoginResponse>("sarctkn"));
        }


        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse.Meta.Msj);
        }
    }
}
using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Login;
using BCP.CROSS.MODELS.Segurinet;
using BCP.CROSS.SECRYPT;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class AuthService : IAuthService
    {
        private const string ContentType = "application/json";
        private readonly HttpClient _httpClient;
        private readonly IManagerSecrypt _secrypt;
        private readonly IOptions<SegurinetApiSettings> _segurinetApiSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;
        private readonly AuthConnector _authConnector;
        public AuthService(IHttpClientFactory httpClientFactory,
            IManagerSecrypt secrypt,
            IOptions<SegurinetApiSettings> segurinetApiSettings,
            AuthConnector authConnector,
            IHttpContextAccessor httpContextAccessor
        )
        {
            IHttpClientFactory httpclientFactory = httpClientFactory;
            _httpClient = httpclientFactory.CreateClient("CARD_API");
            _secrypt = secrypt;
            _authConnector = authConnector;
            _segurinetApiSettings = segurinetApiSettings;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

        }
    
       
        public async Task<ApiSegurinetResponse> LoginAsync(ApiSegurinetRequest loginRequest)
        {
            var response = await _authConnector.LoginAsyncV2<ApiSegurinetResponse, ApiSegurinetRequest>
                (_segurinetApiSettings.Value.Login, loginRequest);

            return response;
        }
        
        public async Task<ServiceResponse<LoginResponse>> LoginAsyncTest(LoginRequest loginRequest)
        {
            var response = await Task.Run(() =>
                new ServiceResponse<LoginResponse>
                {
                    Data = new LoginResponse
                    {
                        Nombre = "Toro Salas Douglas",
                        Matricula = "T25208",
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

        public async Task<ApiSegurinetResponse> GetToken()
        {
            return await Task.Run(() => _session.Get<ApiSegurinetResponse>("cardtkn"));
        }


        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            var exceptionReponse = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>().ConfigureAwait(false);
            throw new Exception(exceptionReponse.Meta.Msj);
        }
    }
}

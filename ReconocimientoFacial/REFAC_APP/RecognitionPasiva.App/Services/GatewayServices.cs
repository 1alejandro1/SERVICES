using BCP.CROSS.MODELS.Request;
using BCP.CROSS.MODELS.Response;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace RecognitionPasiva.App.Services
{
    public interface IGatewayServices
    {
        Task<HttpResponseWrapper<string>> Token(string sessionID);
        Task<HttpResponseWrapper<ParameterResponse>> Parameter(string sessionID, int channelID);
        Task<HttpResponseWrapper<InstruccionPasivaResponse>> Instruction(string sessionID);
        Task<HttpResponseWrapper<ValidacionPasivaResponse>> Validation(string sessionID, string selfi);
        Task<HttpResponseWrapper<DataSessionResponse>> DataSession(string sessionID);
        Task<HttpResponseWrapper<DocumentLoadResponse>> LoadImage(string sessionID, string carnet, string lado);
        Task<HttpResponseWrapper<BaseRekognitionResponse<InitRekognitionResponse>>> Init(string session, string idc, int width, int height);
        Task<HttpResponseWrapper<BaseRekognitionResponse<VerifyRekognitionResponse>>> Verify(VerifyRequest request);
        Task<HttpResponseWrapper<BaseRekognitionResponse<TimeOutResponse>>> TimeOut(string session);
        Task<HttpResponseWrapper<BaseRekognitionResponse<StateResponse>>> State(string session);
    }
    public class GatewayServices : IGatewayServices
    {
        private readonly IJSRuntime js;
        private readonly HttpClient client;
        private readonly string TOKENKEY = "TOKENKEY";
        public GatewayServices(IJSRuntime js, HttpClient client)
        {
            this.js = js;
            this.client = client;
        }

        public async Task<HttpResponseWrapper<string>> Token(string sessionID)
        {
            string? token = string.Empty;
            try
            {
                var request = new
                {
                    sessionID = sessionID
                };
                var myContent = JsonConvert.SerializeObject(request);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string method = "Auth/Token";
                var response = await client.PostAsync(method, byteContent);
                if (response.IsSuccessStatusCode)
                {
                    token = response.Headers.GetValues("Authorization").First().ToString();
                    return new HttpResponseWrapper<string>(
                    response: token,
                    error: false,
                    httpResponseMessage: new HttpResponseMessage(statusCode: System.Net.HttpStatusCode.OK));
                }
                else
                {
                    return new HttpResponseWrapper<string>(
                       response: token,
                       error: true,
                       httpResponseMessage: response
                    );
                }
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<string>.ResponseError(ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<ParameterResponse>> Parameter(string sessionID, int channelID)
        {
            try
            {
                string method = "Parameter/Parameter";
                var request = new
                {
                    IdChannel = channelID,
                    SessionId = sessionID
                };
                return await PostAsync<ParameterResponse>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<ParameterResponse>.ResponseError(ex.Message);
            }
        }
        public async Task<HttpResponseWrapper<InstruccionPasivaResponse>> Instruction(string sessionID)
        {
            try
            {
                string method = "Pasiva/Instruccion";
                var request = new
                {
                    SessionId = sessionID
                };
                return await PostAsync<InstruccionPasivaResponse>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<InstruccionPasivaResponse>.ResponseError(ex.Message);
            }
        }
        public async Task<HttpResponseWrapper<ValidacionPasivaResponse>> Validation(string sessionID, string selfi)
        {
            try
            {
                string method = "Pasiva/Validacion";
                var request = new
                {
                    sessionID = sessionID,
                    selfi = selfi
                };
                return await PostAsync<ValidacionPasivaResponse>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<ValidacionPasivaResponse>.ResponseError(ex.Message);
            }
        }
        public async Task<HttpResponseWrapper<DataSessionResponse>> DataSession(string sessionID)
        {
            DataSessionResponse? jsonResponse = null;
            try
            {
                string method = "MultiFactor/DataSession";
                var request = new
                {
                    sessionID = sessionID
                };
                return await PostAsync<DataSessionResponse>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<DataSessionResponse>.ResponseError(ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<DocumentLoadResponse>> LoadImage(string sessionID, string carnet, string lado)
        {
            try
            {
                string method = "MultiFactor/LoadImage";
                var request = new
                {
                    SessionId = sessionID,
                    Carnet = carnet,
                    Lado = lado
                };
                return await PostAsync<DocumentLoadResponse>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<DocumentLoadResponse>.ResponseError(ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<BaseRekognitionResponse<InitRekognitionResponse>>> Init(string session, string idc, int width, int height)
        {
            try
            {
                string method = "MultiFactor/Init";
                var request = new
                {
                    sessionID = session,
                    idc = idc,
                    width = width,
                    height = height
                };
                return await PostAsync<BaseRekognitionResponse<InitRekognitionResponse>>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<BaseRekognitionResponse<InitRekognitionResponse>>.ResponseError(ex.Message);
            }
        }

        public async Task<HttpResponseWrapper<BaseRekognitionResponse<VerifyRekognitionResponse>>> Verify(VerifyRequest request)
        {
            try
            {
                string method = "MultiFactor/Verify";
                return await PostAsync<BaseRekognitionResponse<VerifyRekognitionResponse>>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<BaseRekognitionResponse<VerifyRekognitionResponse>>.ResponseError(ex.Message);
            }   
        }
        public async Task<HttpResponseWrapper<BaseRekognitionResponse<TimeOutResponse>>> TimeOut(string session)
        {
            try
            {
                string method = "MultiFactor/TimeOut";
                var request = new
                {
                    SessionId = session
                };
                return await PostAsync<BaseRekognitionResponse<TimeOutResponse>>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<BaseRekognitionResponse<TimeOutResponse>>.ResponseError(ex.Message);
            }
        }
        public async Task<HttpResponseWrapper<BaseRekognitionResponse<StateResponse>>> State(string session)
        {
            try
            {
                string method = "MultiFactor/State";
                var request = new
                {
                    sessionID = session
                };
                return await PostAsync<BaseRekognitionResponse<StateResponse>>(method, request);
            }
            catch (Exception ex)
            {
                return HttpResponseWrapper<BaseRekognitionResponse<StateResponse>>.ResponseError(ex.Message);
            }
        }
        private async Task<HttpResponseWrapper<T>> PostAsync<T>(string method, object request, bool sintoken = false)
        {
            try
            {
                var token = await js.InvokeAsync<string>("sessionStorage.getItem", TOKENKEY);
                var myContent = JsonConvert.SerializeObject(request);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (!sintoken)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                }
                var response = await client.PostAsync(method, byteContent);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        return new HttpResponseWrapper<T>(
                            response: JsonConvert.DeserializeObject<T>(data),
                            error: false,
                            httpResponseMessage: new HttpResponseMessage(statusCode: System.Net.HttpStatusCode.OK)
                        );
                    }
                }
                return new HttpResponseWrapper<T>(
                            response: default(T),
                            error: true,
                            httpResponseMessage: response
                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

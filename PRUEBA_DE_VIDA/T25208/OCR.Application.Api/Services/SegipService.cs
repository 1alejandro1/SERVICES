using System.Net.Http;

namespace OCR.Application.Api.Services
{
    public class SegipService
    {
        private readonly HttpClient httpClient;
        public SegipService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            httpClient = new HttpClient(clientHandler);
        }
    }
}

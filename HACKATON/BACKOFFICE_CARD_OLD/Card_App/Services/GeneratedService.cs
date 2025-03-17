using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Generated;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class GeneratedService : IGeneratedService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly CardApiConnector _cardcApi;
        private readonly IAuthService _authService;

        public GeneratedService(IOptions<CardApiSettings> cardApiSettings, CardApiConnector cardApi, IAuthService authService)
        {
            _cardApiSettings = cardApiSettings;
            _cardcApi = cardApi;
            _authService = authService;
        }
        public async Task<GeneratedResponse> Generated(GeneratedRequest Request)
        {            
            var response = await _cardcApi.PostGeneratedAsync<GeneratedResponse, GeneratedRequest>
                (_cardApiSettings.Value.CardGenerated, Request);

            return response;
        }
    }
}

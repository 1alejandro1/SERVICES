using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Enroll;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class EnrollService : IEnrollService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly CardApiConnector _cardcApi;
        private readonly IAuthService _authService;
        public EnrollService(IOptions<CardApiSettings> cardApiSettings, CardApiConnector cardApi, IAuthService authService)
        {
            _cardApiSettings = cardApiSettings;
            _cardcApi = cardApi;
            _authService = authService;
        }
        public async Task<ServiceResponse<bool?>> Enroll(EnrollRequest Request)
        {
            var response = await _cardcApi.PostV2Async<bool?, EnrollRequest>
                (_cardApiSettings.Value.ServiceEnroll, Request);

            return response;
        }
    }
}

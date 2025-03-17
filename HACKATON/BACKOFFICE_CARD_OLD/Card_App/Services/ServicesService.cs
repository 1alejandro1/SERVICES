using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Service;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly CardApiConnector _cardcApiConnector;
        private readonly IAuthService _authService;
        public ServicesService(IOptions<CardApiSettings> cardApiSettings, CardApiConnector cardcApiConnector)
        {
            _cardApiSettings = cardApiSettings;
            _cardcApiConnector = cardcApiConnector;
        }
        public async Task<ServiceResponseV2<ListServiceResponse>> ObtieneListServiceAsync(ListServiceRequest request)
        {

            var responseService = await _cardcApiConnector.PostServiceAsync<ListServiceResponse, ListServiceRequest>
                (_cardApiSettings.Value.ServiceList, request);

            return responseService;
        }
    }
}

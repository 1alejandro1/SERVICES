using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Commerce;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class CommerceService : ICommerceService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly CardApiConnector _cardcApiConnector;
        private readonly IAuthService _authService;

        public CommerceService(IOptions<CardApiSettings> cardApiSettings, CardApiConnector cardcApiConnector)
        {
            _cardApiSettings = cardApiSettings;
            _cardcApiConnector = cardcApiConnector;
        }
        public async Task<ServiceResponseV2<CommerceResponse>> ObtieneListCommerceAsync(ListAllCommerceRequest request)
        {

            var responseCommerce = await _cardcApiConnector.PostCommerceAsync<CommerceResponse, ListAllCommerceRequest>(_cardApiSettings.Value.CommerceList, request);

            return responseCommerce;
        }
    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Category;
using BCP.CROSS.MODELS.Commerce;
using BCP.CROSS.MODELS.Lexico;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class LexicoService : ILexicoService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly IOptions<Lexico> _lexico;
        private readonly CardApiConnector _cardApiConnector;
        public LexicoService(
           IOptions<CardApiSettings> cardApiSettings,
           IOptions<Lexico> lexico,
           CardApiConnector cardApiConnector
       )
        {
            _cardApiSettings = cardApiSettings;
            _lexico = lexico;
            _cardApiConnector = cardApiConnector;
        }
        public async Task<IEnumerable<TipoIdc>> GetTipoIdc()
        {
            var tiposIdc = await Task.Run(() => _lexico.Value.TipoIdc);
            return tiposIdc;
        }
        public async Task<IEnumerable<ParametrosError>> GetCardErrors()
        {
            var response = await _cardApiConnector.PostAsync<IEnumerable<ParametrosError>, string>
                (_cardApiSettings.Value.Lexico.TipoError, string.Empty);

            return response.Data;
        }
        public async Task<ServiceResponseV2<BCP.CROSS.MODELS.Category.ListCategoryResponse>> ObtieneListCategoryAsync(ListAllCategoryRequest request)
        {

            var responseCategory = await _cardApiConnector.PostV3Async<ServiceResponseV2<BCP.CROSS.MODELS.Category.ListCategoryResponse>, ListAllCategoryRequest>
                (_cardApiSettings.Value.CategoryList, request);

            return responseCategory;
        }
        public async Task<ServiceResponseV2<CommerceResponse>> ObtieneListCommerceAsync(ListAllCommerceRequest request)
        {
            var responseCommerce = await _cardApiConnector.PostCommerceAsync<CommerceResponse, ListAllCommerceRequest>
                (_cardApiSettings.Value.CommerceList, request);

            return responseCommerce;
        }
    }
}

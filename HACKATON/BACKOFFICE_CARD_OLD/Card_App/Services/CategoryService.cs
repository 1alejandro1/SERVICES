using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Category;
using Card_App.Conectors;
using Card_App.Contratcts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Card_App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IOptions<CardApiSettings> _cardApiSettings;
        private readonly CardApiConnector _cardcApiConnector;
        private readonly IAuthService _authService;

        public CategoryService(
           IOptions<CardApiSettings> cardApiSettings,
           CardApiConnector cardcApiConnector
       )
        {
            _cardApiSettings = cardApiSettings;
            _cardcApiConnector = cardcApiConnector;
        }
        public async Task<ServiceResponseV2<ListCategoryResponse>> ObtieneListCategoryAsync(ListAllCategoryRequest request)
        {

            var responseCategory = await _cardcApiConnector.PostV3Async<ServiceResponseV2<ListCategoryResponse>, ListAllCategoryRequest>
                (_cardApiSettings.Value.CategoryList, request);

            return responseCategory;
        }
    }
}

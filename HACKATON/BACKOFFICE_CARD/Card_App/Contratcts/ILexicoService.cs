using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Category;
using BCP.CROSS.MODELS.Commerce;
using BCP.CROSS.MODELS.Lexico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface ILexicoService
    {
        Task<IEnumerable<TipoIdc>> GetTipoIdc();
        Task<IEnumerable<ParametrosError>> GetCardErrors();
        Task<ServiceResponseV2<BCP.CROSS.MODELS.Category.ListCategoryResponse>> ObtieneListCategoryAsync(ListAllCategoryRequest request);
        Task<ServiceResponseV2<CommerceResponse>> ObtieneListCommerceAsync(ListAllCommerceRequest request);
    }
}

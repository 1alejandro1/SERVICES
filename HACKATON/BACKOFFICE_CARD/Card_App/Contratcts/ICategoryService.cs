using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface ICategoryService
    {
        Task<ServiceResponseV2<ListCategoryResponse>> ObtieneListCategoryAsync(ListAllCategoryRequest request);
    }
}

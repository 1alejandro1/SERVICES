using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Commerce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface ICommerceService
    {
        Task<ServiceResponseV2<CommerceResponse>> ObtieneListCommerceAsync(ListAllCommerceRequest request);
    }
}

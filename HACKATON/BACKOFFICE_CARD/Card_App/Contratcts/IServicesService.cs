using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Service;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface IServicesService
    {
        Task<ServiceResponseV2<ListServiceResponse>> ObtieneListServiceAsync(ListServiceRequest request);
    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.DTOs.PushNotification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface INotificationService
    {
        Task<ServiceResponse<string>> SendNotificationAsync(List<Client> client, int viaEnvioCodigo, string requestId, InsertCasoResponse casoResponse);
    }
}

using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface IClientService
    {
        Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByDbcAsync(GetClientByDbcRequest getClientBydbcRequest);
        Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByIdcAsync(GetClientByIdcRequest getClientByIdcRequest);
        Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByIdcTestAsync(GetClientByIdcRequest getClientByIdcRequest);
    }
}

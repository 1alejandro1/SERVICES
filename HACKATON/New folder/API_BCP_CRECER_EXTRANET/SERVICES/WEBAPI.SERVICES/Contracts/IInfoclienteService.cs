using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IInfoclienteService
    {
        bool[] ValidarIDC(string tipo);
        Task<ServiceResponse<GetClienteResponse>> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId);
        Task<ServiceResponse<List<GetClienteResponse>>> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId);

    }
}

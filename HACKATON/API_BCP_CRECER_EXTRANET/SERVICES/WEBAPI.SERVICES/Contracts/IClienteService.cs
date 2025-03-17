using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IClienteService
    {
        Task<ServiceResponse<List<GetClienteResponse>>> GetClienteByIdcAsync(GetClientByIdcRequest request, string matricula, string requestId);
        Task<ServiceResponse<List<GetClienteResponse>>> GetClienteByDbcAsync(GetClientByDbcRequest request, string requestId);
    }
}

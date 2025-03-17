using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IClienteRepository
    {
        Task<List<GetClienteResponse>> GetClienteByDbcAsync(GetClientByDbcRequest request);
        Task<List<GetClienteResponse>> GetClienteByIdcAsync(GetClientByIdcRequest request);
        Task<List<GetClienteResponse>> GetClienteByIdcReclamosAsync(GetClientByIdcRequest request);
    }
}

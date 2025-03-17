using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IInfoclientePjRepository
    {
        bool ValidarIdc(string tipoIdc);
        Task<GetClientesResponseIntermedio> GetClientePjByIdcAsync(GetClientByIdcRequest request, string matricula, string requestId);
        Task<GetClientesResponseIntermedio> GetClientePjByDbcAsync(GetClientByDbcRequest request, string requestId);
    }
}

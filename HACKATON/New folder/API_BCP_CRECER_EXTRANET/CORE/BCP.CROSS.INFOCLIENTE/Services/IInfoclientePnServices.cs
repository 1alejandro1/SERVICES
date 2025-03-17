using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTE.Services
{
    public interface IInfoclientePnServices
    {
        bool ValidarIdc(string tipoIdc);
        Task<GetClienteResponseIntermedio> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId);
        Task<GetClientesResponseIntermedio> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId);
    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Historico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IHistoricoService
    {
        Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByIdcAllAsync(ClienteIdcRequest request, string requestId);
        Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByIdcFechaAllAsync(ClienteIdcFechaRequest request, string requestId);
        Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByDbcAllAsync(ClienteDbcRequest request, string requestId);
        Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByDbcFechaAllAsync(ClienteDbcFechaRequest request, string requestId);
    }
}

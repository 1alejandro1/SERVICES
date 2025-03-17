using BCP.CROSS.MODELS.Historico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IHistoricoRepository
    {
        Task<List<CasoDTOHistorico>> GetCasoHistoricoByIdcAllAsync(string ClienteIdc, string ClienteTipo, string ClienteExtension);
        Task<List<CasoDTOHistorico>> GetCasoHistoricoByIdcFechaAllAsync(string ClienteIdc, string ClienteTipo, string ClienteExtension, string FechaInicio, string FechaFinal);
        Task<List<CasoDTOHistorico>> GetCasoHistoricoByDbcAllAsync(ClienteDbcRequest request);
        Task<List<CasoDTOHistorico>> GetCasoHistoricoByDbcFechaAllAsync(ClienteDbcFechaRequest request);
    }
}

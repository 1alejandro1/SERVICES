using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ISwampRepository
    {
        Task<DatosBasicosTicketResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request);
        Task<bool> InsertEventAsync(InsertEventoRequest request);
        Task<TipoCambio> GetTipoCambioSwampAsync();
    }
}

using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.SWAMP
{
    public interface ISwamp
    {
        Task<DatosBasicosTicketResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request);
        Task<bool> InsertEventAsync(InsertEventoRequest request);
        Task<TipoCambio> GetTipoCambioSwampAsync();
    }
}

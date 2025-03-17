using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IWcfSwampRepository
    {
        Task<CobroPRResponse> RealizarCobroAsync(CobroPR request);
        Task<DevolucionPRResponse> RealizarAbonoAsync(DevolucionPR request);
    }
}

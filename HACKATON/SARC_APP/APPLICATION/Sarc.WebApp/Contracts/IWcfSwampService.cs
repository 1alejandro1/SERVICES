using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface IWcfSwampService
    {
        Task<ServiceResponse<bool?>> AbanarAsync(DevolucionPR request);
        Task<ServiceResponse<bool?>> DebitarAsync(CobroPR request);
    }
}

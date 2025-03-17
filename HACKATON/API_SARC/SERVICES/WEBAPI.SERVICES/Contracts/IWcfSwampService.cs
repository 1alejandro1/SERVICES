using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IWcfSwampService
    {
        Task<ServiceResponse<bool>> RealizarAbonoAsync(DevolucionPR request, string requestId);
        Task<ServiceResponse<bool>> RealizarCobroAsync(CobroPR request, string requestId);
    }
}

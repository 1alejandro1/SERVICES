using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.CROSS.MODELS;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BCP.CROSS.SMARTLINK
{
    public interface ISmartLink
    {
        HealthCheckResult Check();
        Task<TipoCambio> GetTipoCambioAsync();
    }
}

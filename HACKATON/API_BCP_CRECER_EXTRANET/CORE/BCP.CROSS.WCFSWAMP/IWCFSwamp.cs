using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.WCFSWAMP
{
    public interface IWCFSwamp
    {
        HealthCheckResult Check();
        Task<DevolucionPRResponse> AbonosSwamp(AbonosSwamp request);
        Task<CobroPRResponse> CobroSwamp(CobroSwamp request);
        Task<string> GeneraFactura(string ClienteIdc, string ClienteTipo, string ClienteExtension, string cuf);
    }
}

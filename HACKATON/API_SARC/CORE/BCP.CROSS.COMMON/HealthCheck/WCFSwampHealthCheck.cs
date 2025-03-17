using BCP.CROSS.WCFSWAMP;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class WCFSwampHealthCheck:IHealthCheck
    {
        private readonly WCFSwamp _wcfSwamp;
        public WCFSwampHealthCheck(WCFSwamp wcfSwamp)
        {
            this._wcfSwamp = wcfSwamp;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _wcfSwamp.Check();
            return Task.FromResult(response);
        }
    }
}

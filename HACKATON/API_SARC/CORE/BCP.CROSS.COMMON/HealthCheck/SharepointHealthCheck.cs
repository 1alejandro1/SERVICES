using BCP.CROSS.SHAREPOINT;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class SharepointHealthCheck : IHealthCheck
    {
        private readonly Sharepoint _shrepoint;
        public SharepointHealthCheck(Sharepoint shrepoint)
        {
            this._shrepoint = shrepoint;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return  Task.FromResult(this._shrepoint.Check());
        }
    }
}

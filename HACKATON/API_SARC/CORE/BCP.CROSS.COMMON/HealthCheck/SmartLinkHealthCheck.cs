using BCP.CROSS.SMARTLINK;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class SmartLinkHealthCheck : IHealthCheck
    {
        private readonly SmartLink _smartLink;
        public SmartLinkHealthCheck(SmartLink smartLink)
        {
            this._smartLink = smartLink;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this._smartLink.Check());
        }
    }
}

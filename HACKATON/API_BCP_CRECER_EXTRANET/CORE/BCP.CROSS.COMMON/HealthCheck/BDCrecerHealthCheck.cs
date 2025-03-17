using BCP.CROSS.DATAACCESS;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class BDCrecerHealthCheck : IHealthCheck
    {
        private readonly BD_BCPCRECER _bdCrecer;

        public BDCrecerHealthCheck(BD_BCPCRECER bdCrecer)
        {
            _bdCrecer = bdCrecer;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _bdCrecer.Check();
            return Task.FromResult(response);
        }
    }
}

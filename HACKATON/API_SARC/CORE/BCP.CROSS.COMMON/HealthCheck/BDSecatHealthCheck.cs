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
    public class BDSecatHealthCheck : IHealthCheck
    {
        private readonly BD_SECAT _bdSecat;

        public BDSecatHealthCheck(BD_SECAT bdSecat)
        {
            _bdSecat = bdSecat;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _bdSecat.Check();
            return Task.FromResult(response);
        }
    }
}

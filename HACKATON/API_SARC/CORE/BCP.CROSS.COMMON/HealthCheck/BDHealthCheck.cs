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
    public class BDHealthCheck : IHealthCheck
    {
        private readonly BD_SARC _dbSarc;

        public BDHealthCheck(BD_SARC dbSarc)
        {
            _dbSarc = dbSarc;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _dbSarc.Check();
            return Task.FromResult(response);
        }
    }
}

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
    public class BDSrvSwampHealthCheck: IHealthCheck
    {
        private readonly BD_SERVICIOS_SWAMP _dbSrvSwamp;

        public BDSrvSwampHealthCheck(BD_SERVICIOS_SWAMP dbSrvSwamp)
        {
            _dbSrvSwamp = dbSrvSwamp;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _dbSrvSwamp.Check();
            return Task.FromResult(response);
        }
    }
}

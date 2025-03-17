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
    public class BDRepExtHealthCheck: IHealthCheck
    {
        private readonly BD_REPEXT _dbRepExt;

        public BDRepExtHealthCheck(BD_REPEXT dbRepExt)
        {
            _dbRepExt = dbRepExt;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = _dbRepExt.Check();
            return Task.FromResult(response);
        }
    }
}

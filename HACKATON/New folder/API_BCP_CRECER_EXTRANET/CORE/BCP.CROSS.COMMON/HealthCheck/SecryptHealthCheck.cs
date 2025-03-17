using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class SecryptHealthCheck : IHealthCheck
    {
        private readonly IManagerSecrypt secrypt;

        public SecryptHealthCheck(IManagerSecrypt secrypt)
        {
            this.secrypt = secrypt;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var check = secrypt.Check();
            if (check.Item1)
                return Task.FromResult(HealthCheckResult.Healthy(check.Item2));
            else
                return Task.FromResult(HealthCheckResult.Unhealthy(check.Item2));
        }
    }
}

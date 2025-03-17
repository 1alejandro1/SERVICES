using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public static class ProcessMonitorHealthCheckBuilderExtensions
    {
        /// <summary>
        /// Regster the Process monitor Health check.
        /// </summary>
        /// <param name="builder">Health check builder.</param>
        /// <param name="processName">Name of the process to monitor.</param>
        /// <param name="name">Name of the health check.</param>
        /// <param name="failureStatus">Failure status.</param>
        /// <param name="tags">A list of tags that can be used for filtering health checks.</param>
        /// <returns>Health check builder for chaining.</returns>
        public static IHealthChecksBuilder AddProcessMonitorHealthCheck(
            this IHealthChecksBuilder builder,
            string processName = default,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default)
        {
            return builder.Add(new HealthCheckRegistration(
               name ?? "ProcessMonitor",
               sp => new ProcessMonitorHealthCheck(processName),
               failureStatus,
               tags));
        }
    }
}

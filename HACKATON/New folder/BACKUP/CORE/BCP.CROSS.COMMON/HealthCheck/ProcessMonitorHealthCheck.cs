using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class ProcessMonitorHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Field to hold process name.
        /// </summary>
        private readonly string processName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMonitorHealthCheck"/> class.
        /// </summary>
        /// <param name="processName">Name of the process to monitor.</param>
        public ProcessMonitorHealthCheck(string processName) => this.processName = processName;

        /// <summary>
        /// Check health.
        /// </summary>
        /// <param name="context">Health check context.</param>
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>Health Check result.</returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Process[] pname = Process.GetProcessesByName(this.processName);
            if (pname.Length == 0)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, description: $"Process with the name {this.processName} is not running."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }
        }
    }
}

using BCP.Framework.Logs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class LogsDirectoryHealthCheck : IHealthCheck
    {
        private readonly LogsSettings _logsSettings;

        public LogsDirectoryHealthCheck(IOptions<LogsSettings> logsSettings)
        {
            _logsSettings = logsSettings.Value;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Directory.Exists(_logsSettings.PathFile))
                {
                    return Task.FromResult(HealthCheckResult.Healthy($"Log Directory exist: {_logsSettings.PathFile}"));
                }

                return Task.FromResult(HealthCheckResult.Unhealthy($"Log Directory not exist: {_logsSettings.PathFile}"));
            }
            catch (Exception ex)
            {

                return Task.FromResult(HealthCheckResult.Unhealthy($"Faild find Directory Logs: {_logsSettings.PathFile}, Exception: {ex.Message}"));
            }
        }

    }
   
}

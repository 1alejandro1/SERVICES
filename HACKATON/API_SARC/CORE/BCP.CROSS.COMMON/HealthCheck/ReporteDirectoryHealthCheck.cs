using BCP.CROSS.REPORTES;
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
    public class ReporteDirectoryHealthCheck : IHealthCheck
    {
        private readonly ReporteSettings _reporteSettings;

        public ReporteDirectoryHealthCheck(IOptions<ReporteSettings> reporteSettings)
        {
            _reporteSettings = reporteSettings.Value;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Directory.Exists(_reporteSettings.PathReporte))
                {
                    return Task.FromResult(HealthCheckResult.Healthy($"Reporte Directory exist: {_reporteSettings.PathReporte}"));
                }

                return Task.FromResult(HealthCheckResult.Unhealthy($"Reporte Directory not exist: {_reporteSettings.PathReporte}"));
            }
            catch (Exception ex)
            {

                return Task.FromResult(HealthCheckResult.Unhealthy($"Reporte find Directory Logs: {_reporteSettings.PathReporte}, Exception: {ex.Message}"));
            }
        }

    }
}

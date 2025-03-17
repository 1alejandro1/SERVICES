using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class SericesMonitorHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Field to hold process name.
        /// </summary>
        private readonly string serviceName;
        private readonly IHttpClientFactory httpClientFactory;
        private HttpClient _httpClient;
        private readonly string client;
        private readonly string urlService;

        public SericesMonitorHealthCheck(IHttpClientFactory httpClientFactory, string serviceName, string client, string urlService)
        {
            this.httpClientFactory = httpClientFactory;
            this.serviceName = serviceName;
            this.client = client;
            this.urlService = urlService;
        }


        /// <summary>
        /// Check health.
        /// </summary>
        /// <param name="context">Health check context.</param>
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>Health Check result.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClientFactory.CreateClient(this.client);
            var response = await _httpClient.GetAsync(this.urlService, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Running {this.serviceName} Api => Url: {this.urlService}");
            }

            return HealthCheckResult.Unhealthy($"Not Running Push Notification Api  => Url: {this.urlService}");
        }
    }
}

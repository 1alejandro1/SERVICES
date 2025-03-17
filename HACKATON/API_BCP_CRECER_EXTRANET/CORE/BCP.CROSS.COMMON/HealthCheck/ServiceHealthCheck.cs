using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class ServiceHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private HttpClient _httpClient;
        public ServiceHealthCheck(IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> authSettings)
        {
            this.httpClientFactory = httpClientFactory;
            _appSettings = authSettings;
            
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClientFactory.CreateClient("PUSH_NOTIFICATION");
            var response = await _httpClient.GetAsync(_appSettings.Value.ApiPushNotificationSettings.CheckEndpoint, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Running Push Notification Api => Url: {_appSettings.Value.ApiPushNotificationSettings.CheckEndpoint}");
            }

            return HealthCheckResult.Unhealthy($"Not Running Push Notification Api  => Url: {_appSettings.Value.ApiPushNotificationSettings.CheckEndpoint}");
        }
    }
}

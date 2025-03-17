using BCP.CROSS.SWAMP;
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
    public class ServiceSwampHealthCheck : IHealthCheck
    {
        private HttpClient _httpClient;
        private readonly SwampSettings _swampSettings;
        public ServiceSwampHealthCheck(IHttpClientFactory httpClientFactory, IOptions<SwampSettings> swampSettings)
        {
            this._httpClient= httpClientFactory.CreateClient("API_SWAMPCORE");
            this._swampSettings = swampSettings.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri($"{this._swampSettings.UriSwamp}/swagger");
            var response = await _httpClient.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Running Api SwampCore => Url: {uri.AbsoluteUri}");
            }

            return HealthCheckResult.Unhealthy($"Not Running Api SwampCore => Url: {uri.AbsoluteUri}");
        }
    }
}

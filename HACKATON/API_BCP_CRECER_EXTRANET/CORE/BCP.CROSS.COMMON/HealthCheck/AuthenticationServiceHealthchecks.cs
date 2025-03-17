using Microsoft.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace BCP.CROSS.COMMON.HealthCheck
{
    public class AuthenticationServiceHealthchecks :IHealthCheck
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private HttpClient _httpClient;
        public AuthenticationServiceHealthchecks(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;

        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClientFactory.CreateClient("AUTHORIZE");
            var urlCheck = this.configuration.GetSection("AuthSettings:UriBasicAuthorization");
            var response = await _httpClient.GetAsync($"{urlCheck.Value}/swagger", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Running WebApi_Authentication => Url: {urlCheck.Value}/swagger");
            }

            return HealthCheckResult.Unhealthy($"Not WebApi_Authentication  => Url: {urlCheck.Value}/swagger");
        }
    }
}

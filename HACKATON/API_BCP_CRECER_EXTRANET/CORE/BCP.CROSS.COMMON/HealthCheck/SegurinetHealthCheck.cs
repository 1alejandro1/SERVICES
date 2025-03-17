using BCP.CROSS.SEGURINET;
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
    public class SegurinetHealthCheck : IHealthCheck
    {
        private readonly Segurinet _segurinet;

        private HttpClient _httpClient;
        private readonly SegurinetSettings _segurinetSettings;

        public SegurinetHealthCheck(Segurinet segurinet, IHttpClientFactory httpClientFactory, IOptions<SegurinetSettings> segurinetSettings)
        {
            this._segurinet = segurinet;
            this._httpClient = httpClientFactory.CreateClient("WCF_SEGURINET");
            this._segurinetSettings = segurinetSettings.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_segurinetSettings.Segurinet2)
            {
                Uri uri = new Uri($"{this._segurinetSettings.UriSegurinet2}");
                var response = await _httpClient.GetAsync(uri, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy($"Running Api Segurinet => Url: {uri.AbsoluteUri}");
                }

                return HealthCheckResult.Unhealthy($"Not Running Api Segurinet => Url: {uri.AbsoluteUri}");
            }
            else
            {
                var response = _segurinet.Check();
                return await Task.FromResult(response);
            }
        }
    }
}

﻿using BCP.CROSS.INFOCLIENTE;
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
    public class ServiceInfoclienteHealthCheck:IHealthCheck
    {
        private HttpClient _httpClient;
        private readonly  InfoclientePn _infoclienteSettings;
        public ServiceInfoclienteHealthCheck(IHttpClientFactory httpClientFactory, IOptions<InfoclientePn> infoclienteSettings)
        {
            this._httpClient = httpClientFactory.CreateClient("API_INFOCLIENTE");
            this._infoclienteSettings = infoclienteSettings.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri($"{this._infoclienteSettings.BaseUrl}/swagger");
            var response = await _httpClient.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Running Api INFOCLIENTE => Url: {uri.AbsoluteUri}");
            }

            return HealthCheckResult.Unhealthy($"Not Running Api INFOCLIENTE => Url: {uri.AbsoluteUri}");
        }
    }
}

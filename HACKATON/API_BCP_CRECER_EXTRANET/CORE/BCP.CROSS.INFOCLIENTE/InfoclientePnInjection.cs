using BCP.CROSS.INFOCLIENTE.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTE
{
    public static class InfoclientePnInjection
    {
        public static IServiceCollection AddInfoclientePn(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InfoclientePn>(configuration.GetSection(nameof(InfoclientePn)));
            services.AddHttpClient<IInfoclientePnServices, InfoclientePnServices>("API_INFOCLIENTE").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            services.AddScoped<IInfoclientePnServices, InfoclientePnServices>();
            services.AddScoped<InfoclientePnl>();
            return services;
        }
    }
}

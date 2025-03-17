using BCP.CROSS.SEGURINET.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET
{
    public static class SegurinetInjection
    {
        public static IServiceCollection AddSegurinet(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SegurinetSettings>(configuration.GetSection(nameof(SegurinetSettings)));
            services.AddHttpClient<ISegurinetServices, SegurinetServices>("WCF_SEGURINET").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            services.AddScoped<ISegurinetServices, SegurinetServices>();
            services.AddScoped<Segurinet>();
            return services;
        }
    }
}

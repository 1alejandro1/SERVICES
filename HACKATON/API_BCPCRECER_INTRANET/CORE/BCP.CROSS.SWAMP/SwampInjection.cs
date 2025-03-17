using BCP.CROSS.SWAMP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SWAMP
{
    public static class SwampInjection
    {
        public static IServiceCollection AddSwamp(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwampSettings>(configuration.GetSection(nameof(SwampSettings)));
            services.AddHttpClient<ISwampServices, SwampServices>("API_SWAMPCORE").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            services.AddScoped<ISwampServices, SwampServices>();
            services.AddScoped<Swamp>();
            return services;
        }
    }
}

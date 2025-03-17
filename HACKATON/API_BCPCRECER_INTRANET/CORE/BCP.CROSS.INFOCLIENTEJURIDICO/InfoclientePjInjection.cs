using BCP.CROSS.INFOCLIENTEJURIDICO.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTEJURIDICO
{
    public static class InfoclientePjInjection
    {
        public static IServiceCollection AddInfoclientePj(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InfoclientePj>(configuration.GetSection(nameof(InfoclientePj)));
            services.AddHttpClient<IInfoclientePjServices, InfoclientePjServices>("API_INFOCLIENTE_JURIDICO").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            services.AddScoped<IInfoclientePjServices, InfoclientePjServices>();
            services.AddScoped<InfoclientePjl>();
            return services;
        }
    }
}

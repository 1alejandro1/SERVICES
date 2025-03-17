using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Security.Authentication;
using WEBAPI.SERVICES.Contracts;
using WEBAPI.SERVICES.Services;

namespace WEBAPI.SERVICES
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSarcApiServices(this IServiceCollection services)
        {
            services.AddScoped<ISmtpService, SmtpService>();
            services.AddScoped<IViasNotificationService, ViasNotificacionService>();
            services.AddScoped<ISegurinetService, SegurinetService>();
            services.AddScoped<IUsuarioService, UsuarioService>(); 
            services.AddHttpClient<INotificationService, NotificationService>("PUSH_NOTIFICATION", c => c.DefaultRequestHeaders.Add("api-version", "1.0"))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                                        
                    };
                });

            services.AddScoped<INotificationService, NotificationService>();

            services.AddHttpClient<IInfoclienteService, IInfoclienteService>("INFOCLIENTE")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,

                    };
                });

            services.AddScoped<IInfoclienteService, InfoclienteService>();
            services.AddScoped<IClienteService, ClienteService>();
            return services;
        }
    }
}

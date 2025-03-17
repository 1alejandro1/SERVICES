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
            services.AddScoped<ICasoService, CasoService>();
            services.AddScoped<IViasNotificationService, ViasNotificacionService>();
            services.AddScoped<ISegurinetService, SegurinetService>();
            services.AddScoped<IServiciosSwampService, ServiciosSwampService>();
            services.AddScoped<IRepExtService, RepExtService>();
            services.AddScoped<ISecatService, SecatService>();
            services.AddScoped<ISarcService, SarcService>(); 
            services.AddScoped<ISmartLinkService, SmartLinkService>();
            services.AddScoped<ISwampService, SwampService>();
            services.AddScoped<IWcfSwampService, WcfSwampService>(); 
            services.AddScoped<ISharepointService, SharepointService>();
            services.AddScoped<IReportesService, ReportesService>(); 
            services.AddScoped<IConsultaAreaService, ConsultaAreaService>(); 
            services.AddScoped<IUsuarioService, UsuarioService>(); 
            services.AddScoped<ICartaService, CartaService>(); 
            services.AddScoped<ICobroService, CobroService>(); 
            services.AddScoped<IDevolucionService, DevolucionService>(); 
            services.AddScoped<IEspecialidadService, EspecialidadService>(); 
            services.AddScoped<IHistoricoService, HistoricoService>(); 
            services.AddScoped<ITarifarioService, TarifarioService>(); 
            services.AddScoped<IProductoServicioService, ProductoServicioService>();
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
            services.AddScoped<IReportesService, ReportesService>();
            return services;
        }
    }
}

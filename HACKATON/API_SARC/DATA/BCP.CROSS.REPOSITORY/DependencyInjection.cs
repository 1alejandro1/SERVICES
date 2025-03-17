using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.REPOSITORY.Rpositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.CROSS.REPOSITORY
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReposytories(this IServiceCollection services)
        {
            services.AddScoped<ICasoRepository, CasoRepository>();
            services.AddScoped<IViasNotificacionRepository, ViasNotificacionRepository>();
            services.AddScoped<ISegurinetRepository, SegurinetRepository>();
            services.AddScoped<IServiciosSwampRepository, ServiciosSwampRepository>(); 
            services.AddScoped<IRepExtRepository, RepExtRepository>();
            services.AddScoped<ISecatRepository, SecatRepository>();
            services.AddScoped<ISarcRepository, SarcRepository>();
            services.AddScoped<ISmartLinkRepository, SmartLinkRepository>();
            services.AddScoped<ISwampRepository, SwampRepository>();
            services.AddScoped<IWcfSwampRepository, WcfSwampRepository>();
            services.AddScoped<ISharepointRepository, SharepointRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IInfoclientePnRepository, InfoclientePnRepository>();
            services.AddScoped<IInfoclientePjRepository, InfoclientePjRepository>();
            services.AddScoped<IReportesRepository, ReportesRepository>(); 
            services.AddScoped<IConsultaAreaRepository, ConsultaAreaRepository>(); 
            services.AddScoped<IUsuarioRepository, UsuarioRepository>(); 
            services.AddScoped<ICartaRepository, CartaRepository>(); 
            services.AddScoped<ICobroRepository, CobroRepository>(); 
            services.AddScoped<IDevolucionRepository, DevolucionRepository>(); 
            services.AddScoped<IEspecialidadRepository, EspecialidadRepository>(); 
            services.AddScoped<IHistoricoRepository, HistoricoRepository>(); 
            services.AddScoped<ITarifarioRepository, TarifarioRepository>(); 
            services.AddScoped<IProductoServicioRepository, ProductoServicioRepository>();
            return services;
        }
    }
}

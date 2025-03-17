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
            services.AddScoped<IViasNotificacionRepository, ViasNotificacionRepository>();
            services.AddScoped<ISegurinetRepository, SegurinetRepository>();
            services.AddScoped<ICrecerRepository, CrecerRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IInfoclientePnRepository, InfoclientePnRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}

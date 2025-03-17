using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.CROSS.DATAACCESS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseSettings>(configuration.GetSection(nameof(DataBaseSettings)));
            services.AddScoped<BD_SARC>();
            services.AddScoped<BD_SECAT>();
            services.AddScoped<BD_SERVICIOS_SWAMP>();
            services.AddScoped<BD_REPEXT>();
            return services;
        }
    }
}

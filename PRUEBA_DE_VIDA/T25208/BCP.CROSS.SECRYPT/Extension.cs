using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.CROSS.SECRYPT
{
    public static class Extension
    {
        private static readonly string segurinetSectionName = "segurinet";
        public static IServiceCollection AddSecrypt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            SecryptOptions options = new SecryptOptions();
            configuration.GetSection(segurinetSectionName).Bind(options);
            services.AddSingleton<IManagerSecrypt, ManagerSecrypt>(sp =>
            {
                return new ManagerSecrypt(options.semilla);
            });
            return services;
        }
    }
}
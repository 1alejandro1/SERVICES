using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.CROSS.SECRYPT
{
    public static class DependencyInjection
    {
        private static readonly string securitySectionName = "SecuritySettings";
        public static IServiceCollection AddSecrypt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            SecryptOptions options = new SecryptOptions();
            configuration.GetSection(securitySectionName).Bind(options);
            services.AddSingleton<IManagerSecrypt, ManagerSecrypt>(sp =>
            {
                return new ManagerSecrypt(options.KeyName);
            });
            return services;
        }
    }
}
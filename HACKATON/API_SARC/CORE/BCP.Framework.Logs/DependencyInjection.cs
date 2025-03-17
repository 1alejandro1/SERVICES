using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Framework.Logs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LogsSettings>(configuration.GetSection(nameof(LogsSettings)));
            services.AddSingleton<ILoggerManager, LoggerManager>();
            return services;
        }
    }
}

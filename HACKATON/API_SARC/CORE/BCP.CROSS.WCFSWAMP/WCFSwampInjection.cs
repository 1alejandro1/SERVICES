using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.WCFSWAMP
{
    public static class WCFSwampInjection
    {
        public static IServiceCollection AddWCFSwamp(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WCFSwampSettings>(configuration.GetSection(nameof(WCFSwampSettings)));
            services.AddScoped<WCFSwamp>();
            return services;
        }
    }
}

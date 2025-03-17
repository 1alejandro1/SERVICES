using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SMARTLINK
{
    public static class SmartLinkInjection
    {
        public static IServiceCollection AddSmartLink(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmartLinkSettings>(configuration.GetSection(nameof(SmartLinkSettings)));
            services.AddScoped<SmartLink>();
            return services;
        }
    }
}

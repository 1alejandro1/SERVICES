using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SHAREPOINT
{
    public static class SharepointInjection
    {
        public static IServiceCollection AddSharepoint(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SharepointSettings>(configuration.GetSection(nameof(SharepointSettings)));
            services.AddScoped<Sharepoint>();
            return services;
        }
    }
}

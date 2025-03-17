using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPORTES
{
    public static class ReporteInjection
    {
        public static IServiceCollection AddReporte(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ReporteSettings>(configuration.GetSection(nameof(ReporteSettings)));
            services.AddScoped<Reporte>();
            return services;
        }
    }
}

using BCP.CORSS.CONNECTORS;
using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Middlewares;
using BCP.CROSS.DATAACCESS;
using BCP.CROSS.INFOCLIENTE;
using BCP.CROSS.INFOCLIENTEJURIDICO;
using BCP.CROSS.REPORTES;
using BCP.CROSS.REPOSITORY;
using BCP.CROSS.SECRYPT;
using BCP.CROSS.SECURITY;
using BCP.CROSS.SEGURINET;
using BCP.CROSS.SHAREPOINT;
using BCP.CROSS.SMARTLINK;
using BCP.CROSS.SWAMP;
using BCP.CROSS.WCFSWAMP;
using BCP.Framework.Logs;
using DinkToPdf;
using DinkToPdf.Contracts;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sarc.WebApi.Extensions;
using Sarc.WebApi.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using WEBAPI.SERVICES;

namespace Sarc.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddMyCors(Configuration);
            services.AddAutoMapper(typeof(ProfileAutoMapper));
            services.AddAutoMapper(typeof(ProfileAutoMapperV2));
            services.AddLogger(Configuration);
            services.AddSecrypt();
            services.AddDataAccess(Configuration);
            services.AddSegurinet(Configuration);
            services.AddSmartLink(Configuration);
            services.AddSwamp(Configuration);
            services.AddInfoclientePn(Configuration);
            services.AddInfoclientePj(Configuration);
            services.AddSharepoint(Configuration);
            services.AddWCFSwamp(Configuration);
            services.AddReporte(Configuration);
            services.AddReposytories();
            services.AddSarcApiServices();
            services.AddSecurity(Configuration);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));//nuevo
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddVersionedApiExplorerExtension();
            services.AddSwaggerGenExtension();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddHealthCheckApi(Configuration);
            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromDays(60);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerExtension(provider);
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors("MyPolicyCors");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = "/monitor";
                });
                endpoints.MapControllers();
            });
        }
    }
}

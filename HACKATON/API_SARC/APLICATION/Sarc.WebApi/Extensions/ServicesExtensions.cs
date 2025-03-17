using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sarc.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using BCP.CROSS.COMMON.HealthCheck;
using System.Net.Http;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BCP.CROSS.SECURITY;
using Microsoft.AspNetCore.Authentication;
using BCP.CROSS.SECURITY.BasicAuthentication;

namespace Sarc.WebApi.Extensions
{
    public static class ServicesExtensions
    {

        public static IServiceCollection ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key"))),
                    ClockSkew = TimeSpan.Zero
                });
            services.AddHttpClient<IBasicAuthenticationService, BasicAuthenticationService>("AUTHORIZE").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            services.AddScoped<IBasicAuthenticationService, BasicAuthenticationService>();
            return services;
        }
        public static void AddMyCors(this IServiceCollection services, IConfiguration configuration) =>
           services.AddCors(options =>
           {
               string[] origenes = configuration.GetValue<string>("ApplicationSettings:AllowedHosts").Split(';');
               options.AddPolicy("MyPolicyCors", builder =>
                   builder.WithOrigins(origenes)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
           });

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });
        }

        public static void AddSwaggerGenExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                /*var securityScheme= new OpenApiSecurityScheme
                {
                    Description = "Enter JWT Bearer token **_only_**",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference =new OpenApiReference
                    {
                        Id= JwtBearerDefaults.AuthenticationScheme,
                        Type=ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, new List<string>()
                    }
                });*/

                var basicSecurityScheme= new OpenApiSecurityScheme
                {
                    Description = "Basic Authorization header using Architecture's Credentials Api Authentication",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference= new OpenApiReference
                    {
                        Id= "BasicAuth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        basicSecurityScheme, new List<string>()
                    }
                });
            });
        }

        public static IServiceCollection AddHealthCheckApi(this IServiceCollection services, IConfiguration configuration)
        {
            Func<IServiceProvider, HttpMessageHandler> configureHttpMessageHandler = (sp => {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                };
            });
            services.AddHealthChecks()
                    .AddCheck<ServiceHealthCheck>("PUSH NOTIFICATION", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<AuthenticationServiceHealthchecks>("AUTHENTICATION", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<SecryptHealthCheck>("SECRYPT API", failureStatus: HealthStatus.Unhealthy, tags: new[] { "SECRYPT" })
                    .AddCheck<BDHealthCheck>("BD_SARC", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DATA BASE" })
                    .AddCheck<BDRepExtHealthCheck>("BD_REPEXT", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DATA BASE" })
                    .AddCheck<BDSrvSwampHealthCheck>("BD_SERVICIOS_SWAMP", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DATA BASE" })
                    .AddCheck<BDSecatHealthCheck>("BD_SECAT", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DATA BASE" })
                    .AddCheck<LogsDirectoryHealthCheck>("LOGS DIRECTORY", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DIRECTORY" })
                    .AddCheck<ReporteDirectoryHealthCheck>("REPORTE DIRECTORY", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "DIRECTORY" })
                    .AddCheck<SegurinetHealthCheck>("WCF SEGURINET", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<SmartLinkHealthCheck>("WCF SMARTLINK", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<ServiceInfoclienteHealthCheck>("API INFOCLIENTE", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<ServiceInfoclienteJuridicoHealthCheck>("API INFOCLIENTE JURIDICO", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<ServiceSwampHealthCheck>("API SWAMPCORE", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<SharepointHealthCheck>("SHAREPOINT", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" })
                    .AddCheck<WCFSwampHealthCheck>("WCF SWAMP", failureStatus: HealthStatus.Unhealthy, tags: new string[] { "SERVICE" });
           
            services.AddHealthChecksUI(setup => {
                setup.SetEvaluationTimeInSeconds(configuration.GetValue<int>("HealthChecksUI:EvaluationTimeOnSeconds"));
                setup.SetMinimumSecondsBetweenFailureNotifications(configuration.GetValue<int>("HealthChecksUI:MinimumSecondsBetweenFailureNotifications"));
                setup.UseApiEndpointHttpMessageHandler(sp =>
                    {
                        return new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                        };
                    });
                }).AddInMemoryStorage();

            return services;
        }
            

    }
}

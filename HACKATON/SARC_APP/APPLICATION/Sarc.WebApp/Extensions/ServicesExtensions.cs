using BCP.CROSS.MODELS.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Sarc.WebApp.Extensions
{
    public static class ServicesExtensions
    {

        public static IServiceCollection AddSarcAuth(this IServiceCollection services, IConfiguration configuration)
        {


            List<Politica> politicas = new();
            configuration.GetSection("SegurinetSettings:politicas").Bind(politicas);

            services.AddAuthorization(options =>
            {
                foreach (var politica in politicas)
                {
                    options.AddPolicy(politica.nombre, policy => policy.RequireClaim("Role", politica.roles));
                }
                options.AddPolicy("MY_POLICY", policy => policy.RequireClaim("Role", "MY_ROLE"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandlerManager>(JwtBearerDefaults.AuthenticationScheme, null)
                .AddCookie(IdentityConstants.ApplicationScheme);

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("SARC_API", options =>
            {
                options.BaseAddress = new Uri(configuration.GetValue<string>("SarcApiSettings:BaseUrl"));
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                options.Timeout = TimeSpan.FromMinutes(3);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler()
                        {
                            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                        };
                    }
              );

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<SarcApiConnector>();
            services.AddTransient<ICaseService, CaseService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ILexicoService, LexicoService>();
            services.AddTransient<IReportesService, ReportesService>();
            services.AddTransient<IFileService, FileService> ();
            services.AddTransient<IWcfSwampService, WcfSwampService> ();

            return services;
        }

        /// <summary>
        /// The Retry policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        private static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            Random random = new();
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(5, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)) + TimeSpan.FromMilliseconds(random.Next(0, 100)));
            return retryPolicy;
        }

        /// <summary>
        /// Gets the circuit breaker policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        private static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
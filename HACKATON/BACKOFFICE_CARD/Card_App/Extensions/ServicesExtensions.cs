using BCP.CROSS.MODELS.Login;
using Card_App.Conectors;
using Card_App.Contratcts;
using Card_App.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Card_App.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCardAuth(this IServiceCollection services, IConfiguration configuration)
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
            services.AddHttpClient("CARD_API", options =>
            {
                options.BaseAddress = new Uri(configuration.GetValue<string>("CardApiSettings:BaseUrl"));
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
            services.AddTransient<IGeneratedService, GeneratedService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommerceService, CommerceService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<IEnrollService, EnrollService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ILexicoService, LexicoService>();
            services.AddTransient<CardApiConnector>();
            services.AddTransient<AuthConnector>();


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

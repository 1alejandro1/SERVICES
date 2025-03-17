using BCP.CROSS.SECURITY.BasicAuthentication;
using BCP.CROSS.SEGURINET;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;

namespace BCP.CROSS.SECURITY
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddHttpClient<IBasicAuthenticationService, BasicAuthenticationService>("AUTHORIZE").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; },
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
            });
            List<Politica> politicas = new List<Politica>();
;           configuration.GetSection("SegurinetSettings:politicas").Bind(politicas);
            services.AddAuthorization(options =>
            {
                foreach(var politica in politicas)
                {
                    options.AddPolicy(politica.nombre, policy => policy.RequireClaim("Role",politica.roles));
                }
            });
            services.AddScoped<IBasicAuthenticationService, BasicAuthenticationService>();
            return services;
        }
    }
}

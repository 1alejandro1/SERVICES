using BCP.CROSS.SECURITY;
using BCP.CROSS.SECURITY.BasicAuthentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;

namespace OCR.Application.Api.Extensions
{
    public static class ServicesExtensions
    {
        #region Configuracion de servicios de accessibilidad a la api

        public static void AddConfigureJWT(this IServiceCollection services, IConfiguration configuration) =>

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                });
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
        public static void AddConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
           services.AddCors(options =>
           {
               var origenesPermitidos = configuration["ApplicationSettings:AllowedHosts"].Split(';');
               options.AddPolicy("PolicyEdd", builder =>
                   builder.WithOrigins(origenesPermitidos)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
           });
        #endregion
               
    }
}

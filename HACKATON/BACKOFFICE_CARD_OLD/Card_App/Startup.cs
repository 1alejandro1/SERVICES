﻿using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Middlewares;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Card_App.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Card_App
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
            });
            services.Configure<CardApiSettings>(Configuration.GetSection("CardApiSettings"));            
            services.Configure<SegurinetApiSettings>(Configuration.GetSection("SegurinetApiSettings"));
            services.Configure<SegurinetSettings>(Configuration.GetSection("SegurinetSettings"));
            services.Configure<Lexico>(Configuration.GetSection("Lexico"));
            services.Configure<SecryptOptions>(Configuration.GetSection("SecuritySettings"));
            services.AddAutoMapper(typeof(ProfileAutoMapper));
            services.AddLogger(Configuration);
            services.AddSecrypt();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".Card.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.IsEssential = true;
            });

            services.AddCardAuth(Configuration);
            services.AddServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Card/Error/500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();
            app.UseStatusCodePages(async context =>
            {
                await Task.Run(() =>
                {
                    var response = context.HttpContext.Response;
                    var urlbase = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.PathBase}";
                    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        response.Redirect($"{urlbase}/Auth/Login");
                    else if (response.StatusCode == (int)HttpStatusCode.Forbidden)
                        response.Redirect($"{urlbase}/Auth/Forbidden");
                });
            });

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

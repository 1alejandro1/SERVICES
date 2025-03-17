using Microsoft.AspNetCore.Authentication.Cookies;
using NETCORE.Services.Interfaces;
using NETCORE.Services;
using MICRE.ABSTRACTION.ENTITIES.Options;
using MICRE.APPLICATION.CONNECTIONS.SERVICES;
using MICRE.APPLICATION.CONNECTIONS.SERVICES.HttpConnectionServices;
using MICRE_APP_EXCEPCIONES.SessionHandler;
using MICRE.ABSTRACTION.SECRYPT;
using MICRE.ABSTRACTION.LOGGER;
using MICRE.APPLICATION.BUSSINESS;
using MICRE_APP_EXCEPCIONES.Commons;


var builder = WebApplication.CreateBuilder(args);

//var authenticationSettings = builder.Configuration.GetSection("Authentication:Api");

var authenticationSettings = builder.Configuration.GetSection("Configuration.Site");

int _minutesSessionExpire = Convert.ToInt32(authenticationSettings["SessionLimit"]);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Habilitar memoria distribuida para la sesión
builder.Services.AddDistributedMemoryCache();

// Configurar la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(_minutesSessionExpire);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddLogger();
builder.Services.AddSecrypt();

// Configurar CORS
builder.Services.AddSingleton<IFormularioInmuebleService, FormularioInmuebleService>();
builder.Services.AddSingleton<IHttpConnectionServices, HttpConnectionServices>();
builder.Services.AddSingleton<IExcepcionesConnectionServices, ExcepcionesConnectionServices>();
builder.Services.AddSingleton<IExcepcionesBussiness, ExcepcionesBussiness>();
builder.Services.AddSingleton<IUserAutonomyVerification, UserAutonomyVerification>();
builder.Services.Configure<ApiExcepcionesOptions>(options => builder.Configuration.GetSection("Connections.Api.Excepciones").Bind(options));
builder.Services.Configure<AuthenticationOptions>(options => builder.Configuration.GetSection("Authentication.Api").Bind(options));
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddScoped<SessionHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAutonomyVerification, UserAutonomyVerification>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = authenticationSettings["LoginPath"];  // Redireccionar al login si no está autenticado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(_minutesSessionExpire);  // Tiempo de expiración de la cookie de autenticación
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.SlidingExpiration = true;
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(_minutesSessionExpire);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}

app.UseSession();
app.MapDefaultControllerRoute();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Habilitar el uso de sesiones
app.UseSession();  // Middleware para sesiones

// Habilitar CORS
app.UseCors("AllowAll");

// Configurar las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}"); // Ruta predeterminada de inicio

// Redirección a la página de login
app.MapGet("/", context =>
{
    context.Response.Redirect("account/login");
    return Task.CompletedTask;
});

app.Run();

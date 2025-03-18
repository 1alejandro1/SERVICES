using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecognitionPasiva.App;
using RecognitionPasiva.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<LoginSesion>();
builder.Services.AddHttpClient<IGatewayServices, GatewayServices>((js,c) =>
{
    c.BaseAddress = new Uri(builder.Configuration["URI_API"]);
    //c.EnableIntercept(sp);
});
#region LoginInstance
builder.Services.AddScoped<LoginServices>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, LoginServices>(provider => provider.GetRequiredService<LoginServices>());
builder.Services.AddScoped<ILoginServices, LoginServices>(provider => provider.GetRequiredService<LoginServices>());
#endregion

var app = builder.Build();
await app.RunAsync();
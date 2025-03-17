using BCP.CROSS.DATAACCESS;
using BCP.CROSS.LOGGER;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Common;
using BCP.Framework.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OCR.Application.Api.Extensions;
using OCR.Application.Api.Helpers;
using OCR.Application.Api.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddConfigureCors(builder.Configuration);
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(nameof(ApplicationSettings)));
builder.Services.ConfigureSecurity(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataBase();
builder.Services.AddSecrypt();
builder.Services.AddLogger();
builder.Services.AddSwaggerExtension();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddScoped<ICarnetIdentidadService, CarnetIdentidadService>();
builder.Services.AddScoped<IDocumentosService, DocumentosService>();
builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(60);
});
var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerExtension(provider);
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();
app.UseCors("PolicyEdd");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

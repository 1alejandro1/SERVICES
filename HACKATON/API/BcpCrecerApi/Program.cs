using ApiLib;
using ApiLib.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<BcpContracts, Services>();

builder.Services.AddSingleton<BcpContracts, Services>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
//}

app.UseHttpsRedirection();

app.MapPost("/login",
(LoginRequest user, BcpContracts service) => Login(user, service))
    .Accepts<LoginRequest>("application/json")
    .Produces<LoginResponse>()

.WithName("login");
app.MapPost("/registroUsuario",
(RegistroRequest request, BcpContracts service) => RegistroUser(request, service)).AllowAnonymous()
    .Accepts<RegistroRequest>("application/json")
    .Produces<RegistroResponse>()
//app.MapPost("/registroUsuario", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (RegistroRequest request, BcpContracts bcpContracts) =>
//{
//    var response = bcpContracts.SignIn(request);
//    return response;
//}).AllowAnonymous()

.WithName("categorias");
app.MapGet("/categorias", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (BcpContracts bcpContracts) =>
{
    var response = bcpContracts.GetCategorias();
    return response;
}).AllowAnonymous()

.WithName("empresas");
app.MapPost("/empresas", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (EmpresasRequest request, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.GetEmpresas(request);
    return response;
}).AllowAnonymous()
.WithName("productos");
app.MapPost("/productos", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (ProductosRequest request, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.GetProductos(request);
    return response;
}).AllowAnonymous()
.WithName("nuevoproducto");
app.MapPost("/nuevoproducto", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (NewProducto producto, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.SetProductos(producto);
    return response;
}).AllowAnonymous()
 .WithName("updateproducto");
app.MapPost("/updateproducto", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (UpdateProducto producto, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.UpdateProducto(producto);
    return response;
}).AllowAnonymous()
         .WithName("deleteproducto");
app.MapPost("/deleteproducto", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (ProductoRequest producto, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.DeleteProducto(producto);
    return response;
}).AllowAnonymous()
.WithName("nuevacategoria");
app.MapPost("/nuevacategoria", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (NewCategoria categoria, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.SetCategoria(categoria);
    return response;
}).AllowAnonymous()
 .WithName("updatecategoria");
app.MapPost("/updatecategoria", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (UpdateCategoria categoria, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.UpdateCategoria(categoria);
    return response;
}).AllowAnonymous()
     .WithName("deletecategoria");
app.MapPost("/deletecategoria", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (CategoriasRequest categoria, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.DeleteCategoria(categoria);
    return response;
}).AllowAnonymous()
.WithName("nuevaempresa");
app.MapPost("/nuevaempresa", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (NewEmpresa empresa, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.SetEmpresa(empresa);
    return response;
}).AllowAnonymous()
 .WithName("updateempresa");
app.MapPost("/updateempresa", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (UpdateEmpresa empresa, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.UpdateEmpresa(empresa);
    return response;
}).AllowAnonymous()
    .WithName("deleteempresa");
app.MapPost("/deleteempresa", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")] (EmpresaRequest empresa, BcpContracts bcpContracts) =>
{
    var response = bcpContracts.DeleteEmpresa(empresa);
    return response;
}).AllowAnonymous();

IResult Login(LoginRequest user, BcpContracts service)
{
    if (!string.IsNullOrEmpty(user.telefono.ToString()) &&
        !string.IsNullOrEmpty(user.password))
    {
        var loggedInUser = service.Login(user);
        
        if (loggedInUser.Role == null || loggedInUser.Role == "") return Results.NotFound("User not found");

        var claims = new[]
        {
            new Claim(ClaimTypes.SerialNumber, loggedInUser.id.ToString()),
            new Claim(ClaimTypes.Email, loggedInUser.email.ToString()),
            new Claim(ClaimTypes.HomePhone, loggedInUser.telefono.ToString()),
            new Claim(ClaimTypes.Name, loggedInUser.nombre.ToString()),
            new Claim(ClaimTypes.Surname, loggedInUser.paterno.ToString()),
            new Claim(ClaimTypes.Role, loggedInUser.Role.ToString()),
            new Claim(ClaimTypes.DateOfBirth, loggedInUser.fecnac.ToString())
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );
       
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var response = new LoginResponse(loggedInUser.id, loggedInUser.email, loggedInUser.telefono, loggedInUser.nombre, loggedInUser.paterno, loggedInUser.materno, loggedInUser.imagen, loggedInUser.fecnac, loggedInUser.idc, loggedInUser.tipoidc, loggedInUser.extidc, loggedInUser.complementoidc, loggedInUser.Role, tokenString);
        return Results.Ok(response);
    }
    return Results.BadRequest("Invalid user credentials");
}
IResult RegistroUser(RegistroRequest request, BcpContracts service)
{
    
        var response = service.SignIn(request);

        if (response.id.ToString() == null || response.id.ToString() == "") return Results.NotFound("No se pudo registrar el usuario");

        var claims = new[]
        {
            new Claim(ClaimTypes.SerialNumber, response.id.ToString()),
            new Claim(ClaimTypes.Email, response.email.ToString()),
            new Claim(ClaimTypes.HomePhone, response.telefono.ToString()),
            new Claim(ClaimTypes.Name, response.nombre.ToString()),
            new Claim(ClaimTypes.Surname, response.paterno.ToString()),
            new Claim(ClaimTypes.Role, response.Role.ToString()),
            new Claim(ClaimTypes.DateOfBirth, response.fecnac.ToString())
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var RegistroResponse = new RegistroResponse(response.id, response.email, response.telefono, response.nombre, response.paterno, response.materno, response.imagen, response.fecnac, response.idc, response.tipoidc, response.extidc, response.complementoidc, response.Role, tokenString);
        return Results.Ok(RegistroResponse);
 
}

app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

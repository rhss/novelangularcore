using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using novelangularcore.Server.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using novelangularcore.Server.Controllers;
using novelangularcore.Server.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

// Registre o DbContext no container de serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Exemplo de redirecionamento
    });

builder.Services.AddControllers();

// CORS pra geral
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// SPA, Angular
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});

var app = builder.Build();

// testando
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Habilitando o CORS de cima
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

// Endpoints
app.MapControllers();

// pra verificar as paginas e erros com mais ou menos detalhes
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.Run();
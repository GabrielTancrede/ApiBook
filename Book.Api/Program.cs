using Book.Application.Extensions;
using Book.Infra.Context;
using Book.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Book",
        Version = "v1",
        Description = "API REST para gerenciamento de livros, autores e gêneros",
        Contact = new OpenApiContact
        {
            Name = "API Book",
            Email = "contato@apibook.com"
        }
    });
});

var connectionString = configuration.GetConnectionString("Default");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);

var assemblies = new[]
{
    Assembly.GetExecutingAssembly(),
    Assembly.Load("Book.Application")
};

builder.Services.AddMediatorAndAutoMapper(assemblies);
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Book v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

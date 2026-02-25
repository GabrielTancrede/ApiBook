using Book.Api.Configurations;
using Book.Api.Middlewares;
using Book.Core.ValueObjects;
using Book.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .SelectMany(e => e.Value!.Errors.Select(err => new
                {
                    Field = e.Key,
                    Message = err.ErrorMessage
                }))
                .ToList();

            var firstError = errors.FirstOrDefault()?.Message ?? "Dados de entrada inválidos.";

            var errorResponse = new ErrorResponse(
                400,
                firstError,
                context.HttpContext.Request.Path.Value ?? string.Empty,
                errors,
                Guid.NewGuid().ToString()
            );

            return new BadRequestObjectResult(errorResponse);
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Book",
        Version = "v1",
        Description = "API REST para gerenciamento de livros, autores e g�neros",
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
builder.Services.ConfigureBaseServices();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseMiddleware<ExceptionHandlingMiddleware>();

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

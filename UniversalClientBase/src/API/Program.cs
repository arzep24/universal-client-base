using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Infrastructure.Data;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Infrastructure.Data.Repositories;
using UniversalClientBase.API.Middlewares;
using UniversalClientBase.Application.Validators;

var builder = WebApplication.CreateBuilder(args);


// 1. CONFIGURACIÓN DE LA BASE DE DATOS (Sqlite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=universal_client.db";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// 2. REGISTRAR EL REPOSITORIO

builder.Services.AddScoped<ISancionRepository, SancionRepository>();
builder.Services.AddScoped<IRevisionRepository, RevisionRepository>();

// 3. Agregar soporte para controladores
builder.Services.AddControllers();


// 4. Agregar Swagger para documentación automática de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. Agregar el validador de FluentValidation para entidades
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SancionValidator>();

var app = builder.Build();

// Agregar el middleware de manejo de excepciones personalizado
app.UseMiddleware<ExceptionMiddleware>();

// 5. Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();

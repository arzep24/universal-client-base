using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Infrastructure.Data;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Infrastructure.Repositories;
using UniversalClientBase.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);


// 1. CONFIGURACIÓN DE LA BASE DE DATOS (Sqlite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=universal_client.db";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// 2. REGISTRAR EL REPOSITORIO

// Cuando se pide IContactRepository, se manda una instancia de ContactRepository
builder.Services.AddScoped<IContactRepository,ContactRepository>(); 

// 3. Agregar soporte para controladores
builder.Services.AddControllers();

// 4. Agregar Swagger para documentación automática de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

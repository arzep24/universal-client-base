using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniversalClientBase.Infrastructure.Data;

namespace API.IntegrationTests.Shared;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1. Eliminamos la configuración de base de datos del Program.cs
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

            // 2. Creamos y abrimos una conexión SQLite en memoria. 
            // Debe permanecer abierta durante toda la prueba para que los datos no se borren.
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // 3. Registramos el DbContext usando la conexión abierta
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connection);
            });

            // 4. Creamos el esquema de tablas antes de que inicien los tests
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated(); 
        });
    }
}
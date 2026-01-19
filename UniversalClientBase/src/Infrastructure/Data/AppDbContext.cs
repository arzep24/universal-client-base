using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    //DBSet = Una Tabla en la base de datos
    public DbSet<Sancion> Sanciones {get; set;}
    public DbSet<Revision> Revisiones {get; set;}

    //Aquí se configuraran reglas especiales de la BD
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Configuración para la entidad Sancion
        modelBuilder.Entity<Sancion>(entity =>
        {
            entity.Ignore(s => s.Importe);
            entity.Ignore(s => s.EstatusSeguimiento);
            entity.Ignore(s => s.RequiereRevision);
            entity.Ignore(s => s.FechaVencimiento);

            // Precisión decimal para el importe (evita errores de redondeo en multas)
            entity.Property(s => s.Importe)
                  .HasPrecision(18, 2);

            // Índice en CuentaId: Mejora drásticamente la velocidad al buscar el historial de un usuario
            entity.HasIndex(s => s.Cuenta);
        });

        // 2. Configuración para la entidad Revision
        modelBuilder.Entity<Revision>(entity =>
        {
            // Índice en CuentaId para búsquedas rápidas de hallazgos previos
            entity.HasIndex(r => r.Cuenta);

            // Podemos asegurar que el nombre del personal externo no sea nulo
            entity.Property(r => r.PersonalExterno)
                  .IsRequired()
                  .HasMaxLength(100);
        });
    }
}
using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    //DBSet = Una Tabla en la base de datos
    public DbSet<Contact> Contacts {get; set;}

    //Aquí se configuraran reglas especiales de la BD
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Core.Entities;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Infrastructure.Data;

public class SancionRepository : ISancionRepository
{
    private readonly AppDbContext _context;

    public SancionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sancion>> GetAllAsync()
    {
        return await _context.Sanciones.AsNoTracking().ToListAsync();
    }

    public async Task<Sancion?> GetByCuentaAsync(string cuenta)
    {
        return await _context.Sanciones
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Cuenta == cuenta);
    }
    public async Task<Sancion?> GetByIdAsync(int id)
    {
        return await _context.Sanciones
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sancion> AddAsync(Sancion sancion)
    {
        _context.Sanciones.Add(sancion);
        await _context.SaveChangesAsync();
        return sancion;
    }

    public async Task UpdateAsync(Sancion sancion)
    {
        _context.Sanciones.Update(sancion);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Sancion>> GetVencidasAsync()
    {
        return await _context.Sanciones
            .AsNoTracking()
            .Where(s => s.FechaVencimiento < DateTime.Now && !s.Pagada)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sancion = await _context.Sanciones.FindAsync(id);
        if (sancion != null)
        {
            _context.Sanciones.Remove(sancion);
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Core.Entities;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Infrastructure.Data;

namespace UniversalClientBase.Infrastructure.Data.Repositories;

public class RevisionRepository : IRevisionRepository
{
    private readonly AppDbContext _context;

    public RevisionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Revision>> GetAllAsync()
    {
        return await _context.Revisiones
            .OrderByDescending(r => r.FechaRevision)
            .ToListAsync();
    }

    public async Task<IEnumerable<Revision>> GetByCuentaAsync(string cuenta)
    {
        return await _context.Revisiones
            .Where(r => r.Cuenta == cuenta)
            .ToListAsync();
    }
    public async Task<Revision?> GetByIdAsync(int id)
    {
        return await _context.Revisiones
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    public async Task<IEnumerable<Revision>> GetByResponsableAsync(string nombre)
    {
        return await _context.Revisiones
            .Where(r => r.Responsable == nombre)
            .ToListAsync();
    }
    public async Task<IEnumerable<Revision>> GetByPersonalExternoAsync(string nombreExterno)
    {
        return await _context.Revisiones
            .Where(r => r.PersonalExterno == nombreExterno)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Revision>> GetByRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        return await _context.Revisiones
            .Where(r => r.FechaRevision >= fechaInicio && r.FechaRevision <= fechaFin)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Revision> AddAsync(Revision revision)
    {
        _context.Revisiones.Add(revision);
        await _context.SaveChangesAsync();
        return revision;
    }

    public async Task UpdateAsync(Revision revision)
    {
        _context.Entry(revision).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var revision = await GetByIdAsync(id);
        if (revision != null)
        {
            _context.Revisiones.Remove(revision);
            await _context.SaveChangesAsync();
        }
    }
}
using UniversalClientBase.Core.Entities;
namespace UniversalClientBase.Core.Interfaces;

public interface IRevisionRepository
{
    Task<IEnumerable<Revision>> GetAllAsync();
    Task<IEnumerable<Revision>> GetByCuentaAsync(string cuenta);
    Task<Revision?> GetByIdAsync(int id);

    Task<IEnumerable<Revision>> GetByResponsableAsync(string nombre);
    Task<IEnumerable<Revision>> GetByPersonalExternoAsync(string nombreExterno);
    Task<Revision> AddAsync(Revision revision);
    Task UpdateAsync(Revision revision);
    Task DeleteAsync(int id);

    Task<IEnumerable<Revision>> GetByRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin);
}
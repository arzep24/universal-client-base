using System.Threading.Tasks;
using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Core.Interfaces;

public interface ISancionRepository 
{
    Task<IEnumerable<Sancion>> GetAllAsync();
    Task<IEnumerable<Sancion>> GetVencidasAsync();
    Task<Sancion?> GetByCuentaAsync(string cuenta);
    Task<Sancion?> GetByIdAsync(int id);
    Task<Sancion> AddAsync(Sancion sancion);
    Task UpdateAsync(Sancion sancion);
    Task DeleteAsync(int id);

}
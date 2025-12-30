using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Core.Interfaces;

public interface IContactRepository
{
    // 1. LEER (Get)
    Task<IEnumerable<Contact>> GetAllAsync();

    Task<Contact?> GetByIdAsync(int id);

    // 2. CREAR (Add)
    Task<Contact> AddAsync(Contact contact);

    // 3. ACTUALIZAR (Update)
    Task UpdateAsync(Contact contact);

    // 4. ELIMINAR (Delete)
    Task DeleteAsync(int d);
}
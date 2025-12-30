using Microsoft.EntityFrameworkCore;
using UniversalClientBase.Core.Entities;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Infrastructure.Data;

namespace UniversalClientBase.Infrastructure.Repositories;

public class ContactRepository: IContactRepository
{
    
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        // Pedimos el AppDbContext. No lo creamos con 'new', alguien más nos lo dará.
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        // Es como escribir en SQL: SELECT * FROM Contacts
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        // Es como escribir en SQL: SELECT * FROM Contacts WHERE Id = {id} LIMIT 1
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<Contact> AddAsync(Contact contact)
    {
        // Agrega el usuario a la memoria local de EF
        await _context.Contacts.AddAsync(contact);
        // Se guardan los cambios en la DB real
        await _context.SaveChangesAsync();

        return contact;
    }

    public async Task UpdateAsync(Contact contact)
    {
        // Se marcala entidad como "Modificada"
        _context.Entry(contact).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if(contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
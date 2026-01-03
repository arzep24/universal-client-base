using Microsoft.AspNetCore.Mvc;
using UniversalClientBase.Application.Dtos;
using UniversalClientBase.Core.Entities;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Core.Dtos;

namespace UniversalClientBase.Controllers;

[ApiController]
[Route("api/[controller]")] // Creando una ruta como: http://localhost.xxxx/api/contact
public class ContactController: ControllerBase
{
    private readonly IContactRepository _repository;

    // Inyectando el repositorio a través del constructor
    public ContactController(IContactRepository repository)
    {
        _repository = repository;
    }

    // ============================================
    // GET: api/contacts
    // Obtener todos los contactos
    // ============================================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetAll([FromQuery] ContactQueryParams queryParams)
    {
        var contacts = await _repository.GetAllAsync(queryParams);

        // Mapeo manual de Entidad a DTO, dado que no queremos exponer la entidad directamente al cliente
        var contactDtos = contacts.Select(c => new ContactDto(
            c.Id,
            c.FirstName,
            c.LastName,
            c.Email,
            c.Phone,
            c.Status,
            c.CreatedAt
        ));
        return Ok(contactDtos);
    }
    // ============================================
    // GET: api/contacts/{id}
    // Obtener un contacto por ID
    // ============================================
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDto>> GetById(int id)
    {
        var contact = await _repository.GetByIdAsync(id);
        
        if(contact == null)
        {
            return NotFound();
        }

        var contactDto = new ContactDto(
            contact.Id,
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.Phone,
            contact.Status,
            contact.CreatedAt
        );
        return Ok(contactDto);
    }
    // ============================================
    // POST: api/contacts
    // Creamos un nuevo contacto
    // ============================================
    [HttpPost]
    public async Task<ActionResult<ContactDto>> Create(ContactDto createDto)
    {
        // 1. Convertir DTO a Entidad para guardarlo en la base de datos
        var contact = new Contact
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Status = "Prospecto",
            CreatedAt = DateTime.UtcNow
        };
        // 2. Guardar en la base de datos
        var createdContact = await _repository.AddAsync(contact);
        // 3. Convertir la entidad creada de nuevo a DTO para devolverla al cliente
        var responseDto = new ContactDto(
            createdContact.Id,
            createdContact.FirstName,
            createdContact.LastName,
            createdContact.Email,
            createdContact.Phone,
            createdContact.Status,
            createdContact.CreatedAt
        );
        return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, responseDto);
    }
    // ============================================
    // PUT: api/contacts/{id}
    // Modificar un contacto existente
    // ============================================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContactDto contactDto)
    {
        // 1. Validar que el ID de la URL coincida con el del cuerpo (seguridad básica)
    if (id != contactDto.Id)
        return BadRequest("El ID de la URL no coincide con el del cuerpo.");

    // 2. Buscar si existe en la BD
    var existingContact = await _repository.GetByIdAsync(id);
    if (existingContact == null)
        return NotFound($"No se encontró el contacto con ID {id}");

    // 3. Actualizar campos (Mapeo manual o con AutoMapper)
    existingContact.FirstName = contactDto.FirstName;
    existingContact.LastName = contactDto.LastName;
    existingContact.Email = contactDto.Email;
    existingContact.Phone = contactDto.Phone;
    
    // 4. Guardar cambios
    await _repository.UpdateAsync(existingContact);

    return NoContent(); // 204 No Content es el estándar para Updates exitosos
    }

    // ============================================
    // DELETE: api/contacts/{id}
    // Eliminar un contacto existente
    // ============================================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // 1. Buscar contacto
        var contact = await _repository.GetByIdAsync(id);
        if (contact == null){
            return NotFound(new {message = $"Contacto con ID {id} no encontrado."});
        }
        // 2. Eliminar contacto del repositorio
        await _repository.DeleteAsync(id);
        return NoContent();
    }


    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        // Provocamos una excepción manual para probar el Middleware
        throw new Exception("Esta es una prueba del Middleware de Errores Globales.");
    }
}
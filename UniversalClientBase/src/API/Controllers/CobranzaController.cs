using Microsoft.AspNetCore.Mvc;
using UniversalClientBase.Core.Entities;
using UniversalClientBase.Core.Interfaces;
using UniversalClientBase.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalClientBase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CobranzaController : ControllerBase
{
    private readonly ISancionRepository _sancionRepository;
    private readonly IRevisionRepository _revisionRepository;

    public CobranzaController(ISancionRepository sancionRepository, IRevisionRepository revisionRepository)
    {
        _sancionRepository = sancionRepository;
        _revisionRepository = revisionRepository;
    }

    // ==========================================
    // SECCIÓN DE SANCIONES (GESTIÓN FINANCIERA)
    // ==========================================

    [HttpGet("sanciones")]
    public async Task<ActionResult<IEnumerable<SancionDto>>> GetAllSanciones()
    {
        var sanciones = await _sancionRepository.GetAllAsync();
        return Ok(sanciones.Select(MapToSancionDto));
    }

    [HttpGet("sanciones/{id}")]
    public async Task<ActionResult<SancionDto>> GetSancionById(int id)
    {
        var sancion = await _sancionRepository.GetByIdAsync(id);
        if (sancion == null) return NotFound();
        return Ok(MapToSancionDto(sancion));
    }

    [HttpGet("sanciones/vencidas")]
    public async Task<ActionResult<IEnumerable<SancionDto>>> GetVencidas()
    {
        var sanciones = await _sancionRepository.GetVencidasAsync();
        return Ok(sanciones.Select(MapToSancionDto));
    }

    [HttpPost("sanciones")]
    public async Task<ActionResult<SancionDto>> CreateSancion(Sancion sancion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _sancionRepository.AddAsync(sancion);
        return CreatedAtAction(nameof(GetSancionById), new { id = created.Id }, MapToSancionDto(created));
    }

    [HttpPut("sanciones/{id}")]
    public async Task<IActionResult> UpdateSancion(int id, Sancion sancion)
    {
        if (id != sancion.Id) return BadRequest("ID de sanción no coincide");
        await _sancionRepository.UpdateAsync(sancion);
        return NoContent();
    }

    [HttpDelete("sanciones/{id}")]
    public async Task<IActionResult> DeleteSancion(int id)
    {
        await _sancionRepository.DeleteAsync(id);
        return NoContent();
    }
    // 4. Resumen de Sanciones por Estatus (Para ver qué falta cobrar hoy)
    [HttpGet("sanciones/estatus/{estatus}")]
    public async Task<ActionResult<IEnumerable<SancionDto>>> GetByEstatus(string estatus)
    {
        var sanciones = await _sancionRepository.GetAllAsync();
        var filtradas = sanciones.Where(s => s.Estatus.Equals(estatus, StringComparison.OrdinalIgnoreCase));
        return Ok(filtradas.Select(s => MapToSancionDto(s)));
    }

    [HttpGet("sanciones/lista-revision")]
    public async Task<ActionResult<IEnumerable<SancionDto>>> GetListaRevision()
    {
        var todas = await _sancionRepository.GetAllAsync();
        
        // Filtramos usando la lógica dinámica de 7 y 14 días
        var urgentes = todas.Where(s => 
            (s.Estatus == "Pendiente" && (DateTime.Now - s.FechaSancion).TotalDays >= 7) ||
            (s.Estatus == "Abonado" && s.FechaUltimoEstatus.HasValue && (DateTime.Now - s.FechaUltimoEstatus.Value).TotalDays >= 14)
        );

        return Ok(urgentes.Select(s => MapToSancionDto(s)));
    }

    // ==========================================
    // SECCIÓN DE REVISIONES (GESTIÓN OPERATIVA)
    // ==========================================

    [HttpGet("revisiones")]
    public async Task<ActionResult<IEnumerable<RevisionDto>>> GetAllRevisiones()
    {
        var revisiones = await _revisionRepository.GetAllAsync();
        return Ok(revisiones.Select(MapToRevisionDto)); // Uso del mapeador centralizado
    }

    [HttpGet("revisiones/cuenta/{cuenta}")]
    public async Task<ActionResult<IEnumerable<RevisionDto>>> GetByCuenta(string cuenta)
    {
        // Obtenemos el resultado del repositorio
        var result = await _revisionRepository.GetByCuentaAsync(cuenta);

        // Mapeamos cada entidad Revision a RevisionDto
        var dtos = result.Select<Revision, RevisionDto>(r => MapToRevisionDto(r));

        return Ok(dtos);
    }

    [HttpGet("revisiones/{id}")]
    public async Task<ActionResult<RevisionDto>> GetRevisionById(int id)
    {
        var revision = await _revisionRepository.GetByIdAsync(id);
        if (revision == null) return NotFound();
        return Ok(MapToRevisionDto(revision));
    }

    [HttpPost("revisiones")]
    public async Task<ActionResult<RevisionDto>> CreateRevision(Revision revision)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _revisionRepository.AddAsync(revision);
        return CreatedAtAction(nameof(GetRevisionById), new { id = created.Id }, MapToRevisionDto(created));
    }

    [HttpPut("revisiones/{id}")]
    public async Task<IActionResult> UpdateRevision(int id, Revision revision)
    {
        if (id != revision.Id) return BadRequest();
        await _revisionRepository.UpdateAsync(revision);
        return NoContent();
    }

    // 1. Filtro por Rango de Fechas (Para el reporte semanal)
    [HttpGet("revisiones/fecha")]
    public async Task<ActionResult<IEnumerable<RevisionDto>>> GetByDateRange([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
    {
        var revisiones = await _revisionRepository.GetByRangoFechasAsync(inicio, fin);
        return Ok(revisiones.Select(r => MapToRevisionDto(r)));
    }

    // 2. Reporte de Productividad por Personal Interno (Ramon, Pedro, Jorge)
    [HttpGet("revisiones/responsable/{nombre}")]
    public async Task<ActionResult<IEnumerable<RevisionDto>>> GetByResponsable(string nombre)
    {
        var revisiones = await _revisionRepository.GetByResponsableAsync(nombre);
        return Ok(revisiones.Select(r => MapToRevisionDto(r)));
    }

    // 3. Auditoría de Empresa Externa (¿Qué hizo la cuadrilla X?)
    [HttpGet("revisiones/externo/{tecnico}")]
    public async Task<ActionResult<IEnumerable<RevisionDto>>> GetByExterno(string tecnico)
    {
        var revisiones = await _revisionRepository.GetByPersonalExternoAsync(tecnico);
        return Ok(revisiones.Select(r => MapToRevisionDto(r)));
    }

    // ==========================================
    // MÉTODOS PRIVADOS (MAPEO)
    // ==========================================

    private static SancionDto MapToSancionDto(Sancion s) => new SancionDto
    {
        Id = s.Id,
        Cuenta = s.Cuenta,
        Importe = s.Importe,
        Estatus = s.Estatus,
        FechaSancion = s.FechaSancion,
        FechaVencimiento = s.FechaVencimiento,
        Alerta = s.EstatusSeguimiento,
        RequiereSeguimiento = s.EstatusSeguimiento.Contains("Urgente") || s.EstatusSeguimiento.Contains("Vencimiento")
    };

    private static RevisionDto MapToRevisionDto(Revision r) => new RevisionDto
    {
        Cuenta = r.Cuenta,
        Hallazgo = r.Hallazgo,
        ResponsableInterno = r.Responsable, // Mapeo de nombre de entidad a DTO
        PersonalExterno = r.PersonalExterno,
        Lectura = r.Lectura,
        FechaRevision = r.FechaRevision
    };
}
namespace UniversalClientBase.Application.Dtos;

public class SancionDto {
    public int Id { get; set; }
    public required string Cuenta { get; set; }
    public decimal ImporteUMA { get; set; }
    public decimal Importe { get; set; }
    public int UMAS { get; set; }
    public int Inciso { get; set; }
    public DateTime FechaSancion { get; set; }
    public DateTime FechaVencimiento { get; set; } // FechaSancion + 7 días
    public string Estatus { get; set; } = "Pendiente"; // Pendiente, Abonado, Liquidado
    public string Alerta { get; set; } = string.Empty; // Aquí irá el texto del seguimiento
    public bool Pagada { get; set; }
     public bool RequiereRevision { get; set; }

     public bool RequiereSeguimiento { get; set; }
}
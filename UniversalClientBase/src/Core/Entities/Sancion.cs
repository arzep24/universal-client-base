using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalClientBase.Core.Entities;

public class Sancion {
    public int Id { get; set;}
    public required string Cuenta { get; set;} // Formato XX-XXX-XXXX-XX
    public decimal ImporteUMA {get; set;}
    public int UMAS {get; set;}
    
    [NotMapped]
    public decimal Importe {
        get => ImporteUMA * UMAS;
        private set {}
    }

    public int Inciso {get; set;}
    public DateTime FechaSancion {get; set;}
    [NotMapped]
    public DateTime FechaVencimiento => FechaSancion.AddDays(7); // FechaSancion + 7 días
    public DateTime? FechaUltimoEstatus {get; set;}
    public string Estatus {get; set;} = "Pendiente"; // Pendiente, Abonado, Liquidado

    public bool Pagada {get; set;} 

    [NotMapped]
    public bool RequiereRevision => (DateTime.Now - FechaSancion).TotalDays > 14 && !Pagada;

    // Propiedad lógica para el Dashboard de la oficina
    [NotMapped]
    public string EstatusSeguimiento 
    {
        get 
        {
            if (Estatus == "Liquidado") return "Cerrado";

            var diasDesdeInicio = (DateTime.Now - FechaSancion).TotalDays;
            
            // Lógica 1: Si sigue pendiente y pasaron 7 días -> IR A REVISAR
            if (Estatus == "Pendiente" && diasDesdeInicio >= 7)
                return "Revision Urgente (7 días sin acercamiento)";

            // Lógica 2: Si ya abonó, revisar si pasaron 14 días desde el abono
            if (Estatus == "Abonado" && FechaUltimoEstatus.HasValue)
            {
                var diasDesdeAbono = (DateTime.Now - FechaUltimoEstatus.Value).TotalDays;
                if (diasDesdeAbono >= 14)
                    return "Vencimiento de Plazo (14 días tras abono)";
            }

            return "En tiempo";
        }
    }
}
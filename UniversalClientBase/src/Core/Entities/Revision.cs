namespace UniversalClientBase.Core.Entities;

public class Revision {
    public int Id {get; set;}
    public required string Cuenta {get; set;}
    public required string Responsable {get; set;} // Ramon, Pedro, Jorge, etc.

    public required string PersonalExterno {get; set;} // Nombre del personal externo que realizó la revisión
    public required string Hallazgo {get; set;} //"Violado", "Reducido", "Normal", etc.
    public int Lectura {get; set;}
    public DateTime FechaRevision {get; set;}
    public bool ProcesadaEnAutomatizacion { get; set; } = false;
}
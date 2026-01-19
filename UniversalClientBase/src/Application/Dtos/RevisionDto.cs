namespace UniversalClientBase.Application.Dtos;

public class RevisionDto
{
    public string Cuenta { get; set; } = string.Empty;
    public string Hallazgo { get; set; } = string.Empty;
    public string ResponsableInterno { get; set; } = string.Empty;
    public string PersonalExterno { get; set; } = string.Empty;
    public int Lectura { get; set; }
    public DateTime FechaRevision { get; set; }
}
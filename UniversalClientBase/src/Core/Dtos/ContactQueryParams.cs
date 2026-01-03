namespace UniversalClientBase.Core.Dtos;

public class ContactQueryParams
{
    // El término para buscar en Email o LastName
    public string? SearchTerm { get; set; }

    public string? Status { get; set; }
}
namespace UniversalClientBase.Application.Dtos;

// DTO de salida
public record ContactDto(
    int Id,
    string FirstName,
    string? LastName,
    string Email,
    string? Phone,
    string Status,
    DateTime CreatedAt
);

//DTO de entrada
public record CreateContactDto(
    string FirstName,
    string Email,
    string? LastName,
    string? Phone,
    string? Status
);

public record UpdateContactDto(
    int Id,
    string FirstName,
    string? LastName,
    string Email,
    string? Phone
);
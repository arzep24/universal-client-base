using System.IO.Compression;
using FluentValidation;
using UniversalClientBase.Application.Dtos;

namespace UniversalClientBase.Application.Validators;

public class CreateContactValidator : AbstractValidator<CreateContactDto>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es obligatorio. ")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 500 caracteres. ");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio. ")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("El número de teléfono no puede exceder los 20 caracteres. ");

        RuleFor(x => x.Status)
            .Must(x => new[] {"Prospecto", "Cliente", "Inactivo"}.Contains(x))
            .WithMessage("El estado debe ser 'Prospecto', 'Cliente' o 'Inactivo'.");
    }
}
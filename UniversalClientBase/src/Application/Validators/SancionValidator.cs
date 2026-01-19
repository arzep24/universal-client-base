using FluentValidation;
using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Application.Validators;

public class SancionValidator : AbstractValidator<Sancion>
{
    public SancionValidator()
    {
        // Validación del formato de cuenta (XX-XXX-XXXX-XX)
        RuleFor(s => s.Cuenta)
            .NotEmpty().WithMessage("La cuenta es obligatoria.")
            .Matches(@"^\d{2}-\d{3}-\d{4}-\d{2}$")
            .WithMessage("El formato de cuenta debe ser 00-000-0000-00.");

        RuleFor(s => s.Importe)
            .GreaterThan(0).WithMessage("El importe debe ser mayor a 0.");

        RuleFor(s => s.Estatus)
            .Must(e => new[] { "Pendiente", "Abonado", "Liquidado" }.Contains(e))
            .WithMessage("El estatus debe ser Pendiente, Abonado o Liquidado.");
    }
}
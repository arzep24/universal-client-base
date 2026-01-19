using FluentValidation;
using UniversalClientBase.Core.Entities;

namespace UniversalClientBase.Application.Validators;

public class RevisionValidator : AbstractValidator<Revision>
{
    public RevisionValidator()
    {
        RuleFor(r => r.Cuenta)
            .NotEmpty().WithMessage("La cuenta es obligatoria.")
            .Matches(@"^\d{2}-\d{3}-\d{4}-\d{2}$");

        RuleFor(r => r.Responsable)
            .NotEmpty().WithMessage("El responsable interno es obligatorio.");

        RuleFor(r => r.PersonalExterno)
            .NotEmpty().WithMessage("El nombre del técnico externo es obligatorio para la auditoría.");

        RuleFor(r => r.Hallazgo)
            .NotEmpty().WithMessage("Debe registrar un hallazgo (ej. VIOLADO, REDUCIDO).");
    }
}
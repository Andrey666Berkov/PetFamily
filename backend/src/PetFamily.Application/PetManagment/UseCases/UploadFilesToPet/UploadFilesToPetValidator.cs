using FluentValidation;
using PetFamily.Application.Dtos.Validators;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;

public class UploadFilesToPetValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetValidator()
    {
        RuleFor(cvr => cvr.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(cvr => cvr.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleForEach(cvr => cvr.Files)
            .SetValidator(new UploadFilesDtoValidator());
    }
}
using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Core.Validators;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UploadFilesToPet;

public class UploadFilesToPetValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetValidator()
    {
        RuleFor(cvr => cvr.PetId)
            .NotEmpty()
            .WithError(ErrorsMy.General.ValueIsRequired());

        RuleFor(cvr => cvr.VolunteerId)
            .NotEmpty()
            .WithError(ErrorsMy.General.ValueIsRequired());

        RuleForEach(cvr => cvr.Files)
            .SetValidator(new UploadFilesDtoValidator());
    }
}
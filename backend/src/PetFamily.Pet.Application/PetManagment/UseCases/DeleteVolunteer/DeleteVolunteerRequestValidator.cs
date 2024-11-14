using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;

namespace PetFamily.Pet.Application.PetManagment.UseCases.DeleteVolunteer;

public class DeleteVolunteerCommandValidator: AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(cvr => cvr.VolunteerId).NotEmpty().WithError(ErrorsMy.General.ValueIsRequired());
    }
    
}
using FluentValidation;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Pet.Application.PetManagment.UseCases.DeleteVolunteer;

public class DeleteVolunteerCommandValidator: AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(cvr => cvr.VolunteerId).NotEmpty().WithError(ErrorsMy.General.ValueIsRequired());
    }
    
}
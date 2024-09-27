using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeleteVolunteer;

public class DeleteVolunteerRequestValidator: AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(cvr => cvr.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
    
}
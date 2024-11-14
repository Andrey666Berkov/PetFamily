using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;

public class UpdateVolunteerInfoRequestValidator : AbstractValidator<UpdateVolunteerInfoCommand>
{
    public UpdateVolunteerInfoRequestValidator()
    {
        RuleFor(cvr => cvr.VolunteerID).NotEmpty().WithError(ErrorsMy.General.ValueIsRequired());
        RuleFor(cvr => cvr.Initials)
            .MustBeValueObject(c => Initials
                .Create(c.FirstName, c.LastName, c.MiddleName));
        
        RuleFor(cvr => cvr.Description).NotEmpty().WithError(ErrorsMy.General.ValueIsRequired());
    }
    
}



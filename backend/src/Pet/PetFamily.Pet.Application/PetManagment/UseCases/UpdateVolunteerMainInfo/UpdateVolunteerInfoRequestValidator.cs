using FluentValidation;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;

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



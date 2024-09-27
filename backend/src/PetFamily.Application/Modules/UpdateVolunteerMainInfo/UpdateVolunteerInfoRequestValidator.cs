using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public class UpdateVolunteerInfoRequestValidator : AbstractValidator<UpdateVolunteerInfoRequest>
{
    public UpdateVolunteerInfoRequestValidator()
    {
        RuleFor(cvr => cvr.VolunteerID).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
    
}

public class UpdateVolunteerInfoDTOValidator : AbstractValidator<UpdateVolunteerInfoDTO>
{
    public UpdateVolunteerInfoDTOValidator()
    {
        RuleFor(cvr => cvr.Initials)
            .MustBeValueObject(c => Initials
                .Create(c.FirstName, c.LastName, c.MiddleName));
        
        RuleFor(cvr => cvr.Description).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
    
}

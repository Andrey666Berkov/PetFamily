using System.Runtime.InteropServices.JavaScript;
using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(cvr => cvr.Email).MustBeValueObject(Email.Create);

        RuleFor(cvr => cvr.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleForEach(cvr => cvr.requisitesDto)
            .MustBeValueObject(r =>
                Requisite.Create(r.Title, r.Description));

        RuleForEach(cvr => cvr.SocialNetworkDto)
            .MustBeValueObject(s =>
                Requisite.Create(s.Name, s.Link));

        RuleFor(cvr => cvr.FirstName).NotNull()
            .WithError(Errors.General.ValueIsInavalid("FirstName"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("FirstName"));
        
        RuleFor(cvr => cvr.Description).NotNull().WithError(Errors.General.ValueIsInavalid("Description"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("Description"));
        
        RuleFor(cvr => cvr.Experience).NotNull().WithError(Errors.General.ValueIsInavalid("Experience"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("Experience"));
        
        RuleFor(cvr => cvr.LastName).NotNull().WithError(Errors.General.ValueIsInavalid("LastName"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("LastName"));;
        
        RuleFor(cvr => cvr.MiddleName).NotNull().WithError(Errors.General.ValueIsInavalid("MiddleName"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("MiddleName"));;
    }
}
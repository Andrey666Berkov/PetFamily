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

        RuleFor(cvr => cvr.FirstName).NotNull().NotEmpty().WithError(Errors.General.ValueIsInavalid("FirstName"));
        RuleFor(cvr => cvr.Description).NotNull().NotEmpty().WithError(Errors.General.ValueIsInavalid("Description"));
        RuleFor(cvr => cvr.Experience).NotNull().NotEmpty().WithError(Errors.General.ValueIsInavalid("Experience"));
        RuleFor(cvr => cvr.LastName).NotNull().NotEmpty().WithError(Errors.General.ValueIsInavalid("LastName"));;
        RuleFor(cvr => cvr.MiddleName).NotNull().NotEmpty().WithError(Errors.General.ValueIsInavalid("MiddleName"));;
    }
}
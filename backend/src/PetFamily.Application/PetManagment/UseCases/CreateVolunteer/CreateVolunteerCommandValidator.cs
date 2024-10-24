using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.PetManagment.UseCases.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(cvr => cvr.Email).MustBeValueObject(Email.Create);

        RuleFor(cvr => cvr.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleForEach(cvr => cvr.RequisitesDto)
            .MustBeValueObject(r =>
                Requisite.Create(r.Title, r.Description));

        RuleForEach(cvr => cvr.SocialNetworkDto)
            .MustBeValueObject(s =>
                Requisite.Create(s.Name, s.Link));


        RuleFor(cvr => cvr.Initional)
            .MustBeValueObject(s =>
                Initials.Create(s.FirstName, s.LastName, s.MiddleName));

        RuleFor(cvr => cvr.Description).NotNull()
            .WithError(Errors.General.ValueIsInavalid("Description"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("Description"));

        RuleFor(cvr => cvr.Experience).NotNull()
            .WithError(Errors.General.ValueIsInavalid("Experience"))
            .NotEmpty().WithError(Errors.General.ValueIsInavalid("Experience"));
    }

}
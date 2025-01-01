using FluentValidation;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.CreateVolunteer;

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
                FullName.Create(s.FirstName, s.LastName, s.MiddleName));

        RuleFor(cvr => cvr.Description).NotNull()
            .WithError(ErrorsMy.General.ValueIsInavalid("Description"))
            .NotEmpty().WithError(ErrorsMy.General.ValueIsInavalid("Description"));

        RuleFor(cvr => cvr.Experience).NotNull()
            .WithError(ErrorsMy.General.ValueIsInavalid("Experience"))
            .NotEmpty().WithError(ErrorsMy.General.ValueIsInavalid("Experience"));
    }

}
using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

public class UpdateVolunteerSocialNetworkValidate:AbstractValidator<UpdateSocialNetworkRequest>
{
    public UpdateVolunteerSocialNetworkValidate()
    {
        RuleFor(vsn=>vsn.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleForEach(snr => snr.SocialNetworks)
            .MustBeValueObject(s => SocialNetwork.Create(s.Name, s.Link));
    }
}
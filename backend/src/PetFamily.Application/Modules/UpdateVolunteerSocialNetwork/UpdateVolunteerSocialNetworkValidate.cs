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
      
    }
}

public class UpdateVolunteerSocialNetworkDTOValidate:AbstractValidator<UpdateSocialNetworkDto>
{
    public UpdateVolunteerSocialNetworkDTOValidate()
    {
        RuleForEach(vsn=>vsn.SocialNetworks)
            .MustBeValueObject(s=>SocialNetwork.Create(s.Name, s.Link));

        
    }
}
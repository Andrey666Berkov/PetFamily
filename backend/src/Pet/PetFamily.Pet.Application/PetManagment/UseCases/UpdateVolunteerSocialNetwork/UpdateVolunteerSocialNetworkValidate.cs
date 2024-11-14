using FluentValidation;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;

public class UpdateVolunteerSocialNetworkValidate 
    :AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateVolunteerSocialNetworkValidate()
    {
        RuleFor(vsn=>vsn.VolunteerId)
            .NotEmpty()
            .WithError(ErrorsMy.General.ValueIsRequired());
        RuleForEach(vsn=>vsn.SocialNetworks)
            .MustBeValueObject(s=>
                SocialNetwork.Create(s.Name, s.Link));

    }
}


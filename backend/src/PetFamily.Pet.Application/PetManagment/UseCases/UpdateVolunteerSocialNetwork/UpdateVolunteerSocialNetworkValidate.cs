using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Validation;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;

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


using PetFamily.Core.Abstractions;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetwork> SocialNetworks) : ICommands;
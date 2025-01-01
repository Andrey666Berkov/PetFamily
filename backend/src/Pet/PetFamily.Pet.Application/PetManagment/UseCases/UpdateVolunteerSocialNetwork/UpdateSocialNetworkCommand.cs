using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetwork> SocialNetworks) : ICommands;
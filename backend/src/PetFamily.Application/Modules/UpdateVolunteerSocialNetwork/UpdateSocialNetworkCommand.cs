using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetwork> SocialNetworks);
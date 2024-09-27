using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkRequest(
    Guid VolunteerId,
    IEnumerable<SocialNetwork> SocialNetworks);
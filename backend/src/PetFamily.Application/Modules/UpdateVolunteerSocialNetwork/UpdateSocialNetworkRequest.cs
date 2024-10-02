using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkRequest(
    Guid VolunteerId,
    UpdateSocialNetworkDto SocialNetworks);
    
public record UpdateSocialNetworkDto(
    IEnumerable<SocialNetwork> SocialNetworks);

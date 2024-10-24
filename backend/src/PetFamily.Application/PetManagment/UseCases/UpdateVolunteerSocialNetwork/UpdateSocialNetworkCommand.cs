using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    IEnumerable<SocialNetwork> SocialNetworks);
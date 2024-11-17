using PetFamily.Pet.Domain.Volunteers.ValueObjects;

namespace PetFamily.Pet.Controllers.Volunteers.Requests;

public record UpdateSocialRequest(
    IEnumerable<SocialNetwork> SocialNetworks);
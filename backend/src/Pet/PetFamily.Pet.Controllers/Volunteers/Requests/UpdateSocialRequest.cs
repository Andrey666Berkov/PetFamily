using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Pet.Controllers.Volunteers.Requests;

public record UpdateSocialRequest(
    IEnumerable<SocialNetwork> SocialNetworks);
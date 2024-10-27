using PetFamily.Domain.Volunteers;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record UpdateSocialRequest(
    IEnumerable<SocialNetwork> SocialNetworks);
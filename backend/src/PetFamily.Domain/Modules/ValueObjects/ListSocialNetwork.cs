using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListSocialNetwork
{
    private ListSocialNetwork()
    {
    }
    public  ListSocialNetwork(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; } = [];

    public static Result<ListSocialNetwork, Error> Create(List<SocialNetwork>? socialNetwork)
    {
        if (socialNetwork is not null)
        {
            ListSocialNetwork listSocialNetwork = new ListSocialNetwork(socialNetwork);
            return listSocialNetwork;
        }
        return Errors.General.ValueIsInavalid("listSocialNetwork");
    }

}
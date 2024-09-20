using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListSocialNetwork
{
    private ListSocialNetwork()
    {
    }
    public  ListSocialNetwork(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; } = [];
    public static  Result<ListSocialNetwork,Error> Create(SocialNetwork socialNetwork)
    {
        ListSocialNetwork listSocialNetwork=new ListSocialNetwork();
        listSocialNetwork.SocialNetworks.Append(socialNetwork);
        return listSocialNetwork;
    }
}
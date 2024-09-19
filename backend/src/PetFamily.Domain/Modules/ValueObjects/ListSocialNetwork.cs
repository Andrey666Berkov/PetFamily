using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListSocialNetwork
{
    private ListSocialNetwork()
    {
    }
    public List<SocialNetwork> SocialNetworks { get; } = [];
    public static  Result<ListSocialNetwork> Create(SocialNetwork spc)
    {
        ListSocialNetwork listSocialNetwork=new ListSocialNetwork();
        listSocialNetwork.SocialNetworks.Add(spc);
        return listSocialNetwork;
    }
}
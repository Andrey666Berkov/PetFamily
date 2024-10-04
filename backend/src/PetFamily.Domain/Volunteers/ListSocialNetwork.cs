using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record ListSocialNetwork
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
    private ListSocialNetwork()
    {
    }
    
    private  ListSocialNetwork(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }
    
    public static Result<ListSocialNetwork, Error> Create(
        IEnumerable<SocialNetwork> socialNetwork)
    {
         return new ListSocialNetwork(socialNetwork);
    }
    
    public static Result<ListSocialNetwork, Error> Empty()
    {
        var listSocialNetwork = new ListSocialNetwork();
        return listSocialNetwork;
    }

}
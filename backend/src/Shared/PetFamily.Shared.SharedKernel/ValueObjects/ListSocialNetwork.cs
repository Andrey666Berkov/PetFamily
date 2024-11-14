using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

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
    
    public static Result<ListSocialNetwork, ErrorMy> Create(
        IEnumerable<SocialNetwork> socialNetwork)
    {
         return new ListSocialNetwork(socialNetwork);
    }
    
    public static Result<ListSocialNetwork, ErrorMy> Empty()
    {
        var listSocialNetwork = new ListSocialNetwork();
        return listSocialNetwork;
    }

}
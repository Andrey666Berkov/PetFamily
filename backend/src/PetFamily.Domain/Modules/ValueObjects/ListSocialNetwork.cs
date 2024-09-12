namespace PetFamily.Domain.Modules;

public record ListSocialNetwork
{
    private ListSocialNetwork()
    {
        
    }
    public List<SocialNetwork> SocialNetwork{ get;  }
}
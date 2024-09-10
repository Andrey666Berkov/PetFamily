namespace PetFamily.Domain.Modules;

public record ListRequisites
{
    public List<Requisite> Requisites{ get; private set; }
    
    
}


public record ListSocialNetwork
{
    public List<SocialNetwork> SocialNetwork{ get; private set; }
}
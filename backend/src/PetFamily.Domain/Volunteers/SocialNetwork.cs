using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record SocialNetwork
{
   public SocialNetwork(string link, string name)
    {
        Link = link;
        Name = name;
    }
    public string Link { get;  }= default!;
    public string Name { get; }= default!;

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInavalid("SocialNetwork_name");
        
        if(string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInavalid("SocialNetwork_link");
        
        return new SocialNetwork(link, name);
    }
    
}
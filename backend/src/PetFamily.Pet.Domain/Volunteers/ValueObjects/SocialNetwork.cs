using CSharpFunctionalExtensions;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.ValueObjects;

public record SocialNetwork
{
   public SocialNetwork(string link, string name)
    {
        Link = link;
        Name = name;
    }
    public string Link { get;  }= default!;
    public string Name { get; }= default!;

    public static Result<SocialNetwork, ErrorMy> Create(string name, string link)
    {
        if(string.IsNullOrWhiteSpace(name))
            return ErrorsMy.General.ValueIsInavalid("SocialNetwork_name");
        
        if(string.IsNullOrWhiteSpace(link))
            return ErrorsMy.General.ValueIsInavalid("SocialNetwork_link");
        
        return new SocialNetwork(link, name);
    }
    
}
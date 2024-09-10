namespace PetFamily.Domain.Modules;

public class SocialNetwork
{
    public SocialNetwork(string link, string name)
    {
        Link = link;
        Name = name;
    }
    public string Link { get; private set; }= default!;
    public string Name { get; private set; }= default!;

    public static Result<SocialNetwork> Create(string name, string link)
    {
        if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(link))
            return Result<SocialNetwork>.Failure("Nme or link cannot be null or empty");
        return Result<SocialNetwork>.Success(new SocialNetwork(link, name));
    }
    
}
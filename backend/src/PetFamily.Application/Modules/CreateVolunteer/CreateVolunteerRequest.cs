using System.Drawing;

namespace PetFamily.Application.Modules.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName, 
    string LastName, 
    string MiddleName,
    string Email, 
    string Description,
    int NumberPhone, 
    int Experience, 
    RequesitDto requisites,
    SocialNetworkDto SocialNetworkDtos);
    
    
    
    public record RequesitDto(string Title, string Description);
    public record SocialNetworkDto(string Title, string Description);
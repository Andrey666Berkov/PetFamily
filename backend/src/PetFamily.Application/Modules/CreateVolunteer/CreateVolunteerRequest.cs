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
    RequesitRequest requisite,
    SocialNetworkRequest socialNetworkRequest);
    
    
    
    public record RequesitRequest(string Title, string Description);
    public record SocialNetworkRequest(string Title, string Description);
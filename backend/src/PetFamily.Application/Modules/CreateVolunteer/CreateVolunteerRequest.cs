using System.Drawing;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;

namespace PetFamily.Application.Modules.CreateVolunteer;

public record CreateVolunteerRequest(
    RequesitInitionalDto Initional,
    string Email,
    string Description,
    string PhoneNumber,
    int Experience,
    IEnumerable<RequesitDto>? requisitesDto,
    IEnumerable<SocialNetworkDto>? SocialNetworkDto);

public record RequesitDto(string Title, string Description);
public record RequesitInitionalDto(
    string FirstName,
    string LastName, 
    string MiddleName);

public record SocialNetworkDto(string Name, string Link);
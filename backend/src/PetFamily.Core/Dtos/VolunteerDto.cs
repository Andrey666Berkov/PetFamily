namespace PetFamily.Core.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }=string.Empty;
    public string LastName { get; init; }=string.Empty;
    public string MiddleName { get; init; }=string.Empty;
    public string Email { get; init; }=string.Empty;
    public string Description { get; init; } =string.Empty;
    public int Experience { get; init; }
    public string PhoneNumber { get; init; }=String.Empty;
    public RequisiteDto[] Requisites { get; set; } = [];
    public bool IsDeleted{ get; set; } 

    public SocialNetworkDto[] SocialNetworks { get; set; } = [];
    public PetDto[] Pets { get; init; } = [];

}

public record RequisiteDto(string Name, string Description);
public record SocialNetworkDto(string Name, string Url);
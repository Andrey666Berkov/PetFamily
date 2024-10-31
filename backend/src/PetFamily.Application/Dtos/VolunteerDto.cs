namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }=string.Empty;

    public string Email { get; init; }=string.Empty;
    public string Description { get; init; } =string.Empty;
    public int Experience { get; init; }
    public string PhoneNumber { get; init; }=String.Empty;
    public RequisiteDto[] Requisites { get; set; } = [];

    public SocialNetworkDto[] SocialNetworks { get; set; } = [];
    public PetDto[] Pets { get; init; } = [];

}

public record RequisiteDto(string Name, string Description);
public record SocialNetworkDto(string Name, string Url);
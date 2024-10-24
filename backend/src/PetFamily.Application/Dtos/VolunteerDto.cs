namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }=string.Empty;

    public string Email { get; init; }=string.Empty;
    public string Description { get; init; } =string.Empty;
    public int Experience { get; init; }
    public int PhoneNumber { get; init; }
    public PetDto[] Pets { get; init; } = [];

}
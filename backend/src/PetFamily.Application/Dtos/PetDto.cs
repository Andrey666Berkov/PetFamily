namespace PetFamily.Application.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string NickName { get; init; } = string.Empty;
    public string PetType { get; init; }=string.Empty;
    public int Position { get; init; }
    public string Description { get; private set; } = default!;
    /* public string? Color { get; private set; }
     public string? InfoHelth { get; private set; }
     public double? Weight { get; private set; }
     public int? Height { get; private set; }
     public int? NumberPhoneOwner { get; private set; }
     public string SpeciesBreed { get; private set; }
     public bool IsCastrated { get; private set; } = false;
     public DateOnly BirthDate { get; private set; }
     public bool IsVaccinated { get; private set; } = false;*/
    public string StatusHelper { get; init; }= string.Empty;
    public string Requisite { get; init; }= string.Empty;
    public string Photos { get; init; }=string.Empty;
}
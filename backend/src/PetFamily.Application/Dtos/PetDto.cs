namespace PetFamily.Application.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string NickName { get; private set; } = string.Empty;
    public string PetType { get; private set; } = string.Empty;
    public int Position { get; private set; }
    public string Description { get; private set; } = default!;
    public string? Color { get; private set; }
    public string? InfoHelth { get; private set; }
    public double? Weight { get; private set; }
    public int? Height { get; private set; }
    public int? NumberPhoneOwner { get; private set; }
    
    public bool IsCastrated { get; private set; } = false;
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; } = false;
    public string StatusHelper { get; private set; } = string.Empty;
    public string Requisites { get; private set; } = string.Empty;
    public Guid Species_Id { get;  init; }

    public Guid Breed_Id { get; init; }
    public PetFileDto[] Files { get; private set; } = null!;
}

public class PetFileDto
{
    public string PathToStorage { get; init; } = string.Empty;
}
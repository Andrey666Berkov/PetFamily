namespace PetFamily.Application.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string NickName { get;  set; } = string.Empty;
    public string PetType { get;  set; }
    public int Position { get;  set; }
    public string Description { get; private set; } = default!;
    public string? Color { get;  set; }
    public string? InfoHelth { get;  set; }
    public int? Height { get;  set; }
    public int? NumberPhoneOwner { get; private set; }
    
    public bool IsCastrated { get;  set; } = false;
    public DateOnly? BirthDate { get;  set; }
    public bool IsVaccinated { get;  set; } = false;
    public int StatusHelper { get;  set; } 
    public string Requisites { get;  set; } = string.Empty;
    public Guid Species_Id { get;  set; }

    public Guid Breed_Id { get; set; }
    public PetFileDto[] Files { get;  set; } = null!;
}

public class PetFileDto
{
    public string PathToStorage { get; init; } = string.Empty;
}
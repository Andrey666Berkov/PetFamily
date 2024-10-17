namespace PetFamily.Application.Modules.AddPet;

public record FileDataDtoCommand(
    Guid VolunteerId,
    IEnumerable<CreateFileCommand> Photos,
    AddressDto Address,
    RequisiteDto requisite,
    string NickName,
    string Description,
    double Weigth,
    int Heigth,
    int NumberPhone,
    bool IsCastrated);

public record CreateFileCommand(Stream Stream, string FilePath, string BacketName);
public record AddressDto(string Street, string Country, string City);
public record RequisiteDto(string Title, string Description);
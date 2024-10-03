namespace PetFamily.Application.Modules.AddPet;

public record FileDataDtoCommand(
    Guid VolId,
    IEnumerable<PhotoDto> Photos,
    AddressDto Address,
    RequisiteDto requisite,
    string NickName,
    string Description);

public record PhotoDto(Stream Stream, string FileName, string ContentType);
public record AddressDto(string Street, string Country, string City);
public record RequisiteDto(string Title, string Description);
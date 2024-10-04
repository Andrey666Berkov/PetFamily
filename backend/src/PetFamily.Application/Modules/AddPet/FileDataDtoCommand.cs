﻿namespace PetFamily.Application.Modules.AddPet;

public record FileDataDtoCommand(
    Guid VolunteerId,
    IEnumerable<CreateFileDto> Photos,
    AddressDto Address,
    RequisiteDto requisite,
    string NickName,
    string Description,
    double Weigth,
    int Heigth,
    int NumberPhone,
    bool IsCastrated);

public record CreateFileDto(Stream Stream, string FilePath);
public record AddressDto(string Street, string Country, string City);
public record RequisiteDto(string Title, string Description);
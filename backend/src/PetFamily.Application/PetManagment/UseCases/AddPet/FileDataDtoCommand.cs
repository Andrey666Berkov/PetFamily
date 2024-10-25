﻿using System.Windows.Input;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.UseCases.AddPet;

public record FileDataDtoCommand(
    Guid VolunteerId,
    AddressDto Address,
    RequisiteDto requisite,
    string NickName,
    string Description,
    double Weigth,
    int Heigth,
    int NumberPhone,
    bool IsCastrated) : ICommands;

public record AddressDto(string Street, string Country, string City);
public record RequisiteDto(string Title, string Description);
﻿using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.UseCases.CreateVolunteer;

public record CreateVolunteerRequest(
    RequesitInitionalDto Initional,
    string Email,
    string Description,
    string PhoneNumber,
    int Experience,
    IEnumerable<RequesitDto>? RequisitesDto,
    IEnumerable<SocialNetworkDto>? SocialNetworkDto)
{
    public  CreateVolunteerCommand CreateCommand()
    {
        return new CreateVolunteerCommand(Initional, Email, Description, PhoneNumber, Experience,
            RequisitesDto, SocialNetworkDto);
    }
}

public record CreateVolunteerCommand(
    RequesitInitionalDto Initional,
    string Email,
    string Description,
    string PhoneNumber,
    int Experience,
    IEnumerable<RequesitDto>? RequisitesDto,
    IEnumerable<SocialNetworkDto>? SocialNetworkDto) : ICommands;

public record RequesitDto(string Title, string Description);
public record RequesitInitionalDto(
    string FirstName,
    string LastName, 
    string MiddleName);

public record SocialNetworkDto(string Name, string Link);
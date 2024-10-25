using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;

public record UpdateVolunteerInfoCommand(
    Guid VolunteerID,
    RequesitInitialDto Initials,
    string Description) : ICommands;

public record RequesitInitialDto(
    string FirstName,
    string LastName, 
    string MiddleName);
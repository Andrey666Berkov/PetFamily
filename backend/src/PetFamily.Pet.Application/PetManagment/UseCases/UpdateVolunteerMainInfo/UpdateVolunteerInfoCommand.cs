using PetFamily.Core.Abstractions;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;

public record UpdateVolunteerInfoCommand(
    Guid VolunteerID,
    RequesitInitialDto Initials,
    string Description) : ICommands;

public record RequesitInitialDto(
    string FirstName,
    string LastName, 
    string MiddleName);
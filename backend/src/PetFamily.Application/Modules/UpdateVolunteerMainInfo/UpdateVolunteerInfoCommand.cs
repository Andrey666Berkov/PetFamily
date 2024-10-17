using PetFamily.Domain.IDs;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public record UpdateVolunteerInfoCommand(
    Guid VolunteerID, 
    RequesitInitialDto Initials,
    string Description);

public record RequesitInitialDto(
    string FirstName,
    string LastName, 
    string MiddleName);
using PetFamily.Domain.IDs;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public record UpdateVolunteerInfoRequest(Guid VolunteerID, UpdateVolunteerInfoDTO Dto);
public record UpdateVolunteerInfoDTO(
    RequesitInitialDto Initials,
    string Description);
    
public record RequesitInitialDto(
    string FirstName,
    string LastName, 
    string MiddleName);
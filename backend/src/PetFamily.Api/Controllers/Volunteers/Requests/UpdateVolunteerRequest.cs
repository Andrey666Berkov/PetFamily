using PetFamily.Application.Modules.UpdateVolunteerMainInfo;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record UpdateVolunteerRequest(
    RequesitInitialDto Initials,
    string Description);
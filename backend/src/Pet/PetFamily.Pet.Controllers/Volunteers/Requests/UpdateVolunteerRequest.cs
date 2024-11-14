
using PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;

namespace PetFamily.Pet.Controllers.Volunteers.Requests;

public record UpdateVolunteerRequest(
    RequesitInitialDto Initials,
    string Description);
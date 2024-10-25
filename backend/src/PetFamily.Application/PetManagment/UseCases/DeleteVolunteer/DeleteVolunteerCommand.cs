using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.UseCases.DeleteVolunteer;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommands;
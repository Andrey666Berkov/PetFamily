using PetFamily.Core.Abstractions;

namespace PetFamily.Pet.Application.PetManagment.UseCases.DeleteVolunteer;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommands;
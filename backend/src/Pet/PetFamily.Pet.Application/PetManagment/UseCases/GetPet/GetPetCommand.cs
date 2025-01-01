using PetFamily.Shared.Core.Abstractions;

namespace PetFamily.Pet.Application.PetManagment.UseCases.GetPet;

public record GetPetCommand(string Bucket, Guid PetId, Guid VolunteerId) : ICommands;
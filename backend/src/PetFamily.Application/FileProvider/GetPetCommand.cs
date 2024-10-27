using PetFamily.Application.Abstractions;

namespace PetFamily.Application.FileProvider;

public record GetPetCommand(string Bucket, Guid PetId, Guid VolunteerId) : ICommands;
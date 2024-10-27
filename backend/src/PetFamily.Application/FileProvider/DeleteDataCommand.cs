using PetFamily.Application.Abstractions;

namespace PetFamily.Application.FileProvider;

public record DeleteDataCommand(Guid VolunteerId, Guid PetId,  string Bucket) : ICommands;
using PetFamily.Core.Abstractions;

namespace PetFamily.Core;

public record DeleteDataCommand(Guid VolunteerId, Guid PetId,  string Bucket) : ICommands;
using PetFamily.Shared.Core.Abstractions;

namespace PetFamily.Shared.Core.File;

public record DeleteDataFile(Guid VolunteerId, Guid PetId,  string Bucket) : ICommands;
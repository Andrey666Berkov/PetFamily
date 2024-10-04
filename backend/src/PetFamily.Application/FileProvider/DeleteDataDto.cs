namespace PetFamily.Application.FileProvider;

public record DeleteDataDto(Guid VolunteerId, Guid PetId,  string Bucket);
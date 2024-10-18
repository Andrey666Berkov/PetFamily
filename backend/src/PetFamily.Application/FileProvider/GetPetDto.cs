namespace PetFamily.Application.FileProvider;

public record GetPetDto(string Bucket, Guid PetId, Guid VolunteerId) ;
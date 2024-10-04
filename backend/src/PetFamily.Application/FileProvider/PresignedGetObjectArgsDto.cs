namespace PetFamily.Application.FileProvider;

public record PresignedGetObjectArgsDto(string Bucket, Guid petId, Guid volunteerId) ;
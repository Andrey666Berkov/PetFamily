namespace PetFamily.Application.FileProvider;

public record PresignedGetObjectArgsDto(string Bucket, Guid Id);
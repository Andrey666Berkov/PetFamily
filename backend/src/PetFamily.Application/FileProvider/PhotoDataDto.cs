namespace PetFamily.Application.FileProvider;

public record PhotoDataDto(IEnumerable<StreamDataDto> PhotoData, string BucketName);

public record StreamDataDto(Stream Stream, string ObjectName);
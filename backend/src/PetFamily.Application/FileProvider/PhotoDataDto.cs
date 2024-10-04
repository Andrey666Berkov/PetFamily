namespace PetFamily.Application.FileProvider;

public record PhotoDataDto(IEnumerable<FileDataDto> PhotoData, string BucketName);

public record FileDataDto(Stream Stream, string FilePath, string BucketName);
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.FileProvider;

public record PhotoDataDto(IEnumerable<FileDataDto> PhotosData, string BucketName);

public record FileDataDto(Stream Stream, FilePath FilePath, string BucketName);
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.FileProvider;

public record PhotoDataDto(IEnumerable<FileDataDto> PhotosData, string BucketName);

public record FileDataDto(Stream Stream, FileInfo Info);
public record FileInfo( FilePath FilePath, string BucketName);
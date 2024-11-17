namespace PetFamily.Core.Dtos;

public record PhotoDataDto(IEnumerable<FileDataDto> PhotosData, string BucketName);

public record FileDataDto(Stream Stream, FileInfoMy InfoMy);
using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Providers;

public interface IFilesProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFilesAsync(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFileAsync(
        PresignedGetObjectArgsDto getObjectDto,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeletePetAsync(
        DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default);
}
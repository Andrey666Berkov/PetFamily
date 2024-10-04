using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFilesProvider
{
    Task<UnitResult<Error>> UploadPhotosAsync(
        PhotoDataDto filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFileAsync(
        PresignedGetObjectArgsDto getObjectDto,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeletePetAsync(
        DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default);
}
using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFileAsync(FileDataDto fileData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFileAsync(
        PresignedGetObjectArgsDto getObjectDto,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeletePetAsync(
        DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default);
}
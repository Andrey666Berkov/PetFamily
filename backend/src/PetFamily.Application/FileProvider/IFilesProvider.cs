using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.FileProvider;

public interface IFilesProvider
{
    Task<UnitResult<Error>> RemoveFiles(
        FileInfo filesInfo,
        CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFilesAsync(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFileAsync(
        GetPetDto getObjectDto,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeletePetAsync(
        DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default);
    
}
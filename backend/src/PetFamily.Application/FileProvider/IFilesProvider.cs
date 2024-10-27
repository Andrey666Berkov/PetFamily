using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.FileProvider;

public interface IFilesProvider
{
    Task<UnitResult<Error>> RemoveFiles(
        FileInfo filesInfo,
        CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<FilePath>, Error>> Handler(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetFileAsync(
        GetPetCommand getObjectCommand,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeletePetAsync(
        DeleteDataCommand deleteDataCommand,
        CancellationToken cancellationToken = default);
    
}
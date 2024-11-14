using CSharpFunctionalExtensions;
using PetFamily.Core.Dtos;

namespace PetFamily.Core;

public interface IFilesProvider
{
    Task<UnitResult<ErrorMy>> RemoveFiles(
        FileInfoMy filesInfoMy,
        CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<FilePath>, ErrorMy>> Handler(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default);

   
    Task<Result<string, ErrorMy>> GetFileAsync(
        PetCommandProvider getObjectCommand,
        CancellationToken cancellationToken = default);
       

    Task<Result<string, ErrorMy>> DeletePetAsync(
        DeleteDataCommand deleteDataCommand,
        CancellationToken cancellationToken = default);
    
}
using CSharpFunctionalExtensions;
using PetFamily.Shared.Core.Dtos;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Shared.Core.File;

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
        DeleteDataFile deleteDataFile,
        CancellationToken cancellationToken = default);
    
}
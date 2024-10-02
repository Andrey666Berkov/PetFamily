using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeletePet;

public class DeletePetUseCase
{
    private readonly IFileProvider _fileProvider;

    public DeletePetUseCase(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> DeleteUseCase(DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default)
    {
        return await _fileProvider.DeletePetAsync(deleteDataDto, cancellationToken);
        
    }
}
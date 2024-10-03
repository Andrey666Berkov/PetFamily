using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeletePet;

public class DeletePetUseCase
{
    private readonly IPhotosProvider _photosProvider;

    public DeletePetUseCase(IPhotosProvider photosProvider)
    {
        _photosProvider = photosProvider;
    }

    public async Task<Result<string, Error>> DeleteUseCase(DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default)
    {
        return await _photosProvider.DeletePetAsync(deleteDataDto, cancellationToken);
        
    }
}
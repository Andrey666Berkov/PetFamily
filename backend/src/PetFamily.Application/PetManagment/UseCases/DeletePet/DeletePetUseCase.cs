using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.PetManagment.UseCases.DeletePet;

public class DeletePetUseCase
{
    private readonly IFilesProvider _filesProvider;
    private readonly IVolunteerRepository _volunteerRepository;

    public DeletePetUseCase(IFilesProvider filesProvider,
        IVolunteerRepository volunteerRepository)
    {
        _filesProvider = filesProvider;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<string, Error>> DeleteUseCase(DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default)
    {
        var volunteerId=VolunteerId.Create(deleteDataDto.VolunteerId);
        var petId=PetId.Create(deleteDataDto.PetId);
        await _volunteerRepository.DeletePet(volunteerId, petId, cancellationToken);
        
        return await _filesProvider
            .DeletePetAsync(deleteDataDto, cancellationToken);
        
        
    }
}
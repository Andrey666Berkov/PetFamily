using CSharpFunctionalExtensions;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.GetPet;

public class GetPetUseCase
{
    private readonly IFilesProvider _filesProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<GetPetUseCase> _logger;
   

    public GetPetUseCase(
        IFilesProvider filesProvider,
        IVolunteerRepository volunteerRepository)
    {
        _filesProvider = filesProvider;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Pet, Error>> GetPet(
        GetPetDto getPetDto,
        CancellationToken cancellationToken)
    {
        //minio
        var petResult= await _filesProvider
            .GetFileAsync(getPetDto, cancellationToken);
        
        //VolunteerRepository
        var volunteerResult=await _volunteerRepository
            .GetById(VolunteerId.Create(getPetDto.VolunteerId),cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var pet=volunteerResult.Value.Pets
            .FirstOrDefault(p=>p.Id.Value==getPetDto.PetId);

        if (pet == null)
            return Errors.General.NotFound(getPetDto.PetId);

        var photoPathResult = FilePath.CreateOfString(petResult.Value);
        var petPhotorResult = PetPhoto.Create(photoPathResult.Value, false);
        var listPhot=new List<PetPhoto>();
        listPhot.Add(petPhotorResult.Value);
        var photos= new ValueObjectList<PetPhoto>(listPhot);
        
        pet.UpdateFilePhotosList(photos);

        return pet;
    }
}
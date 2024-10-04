using CSharpFunctionalExtensions;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
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
        PresignedGetObjectArgsDto presignedGetObjectArgsDto,
        CancellationToken cancellationToken)
    {
        //minio
        var petResult= await _filesProvider
            .GetFileAsync(presignedGetObjectArgsDto, cancellationToken);
        
        
        //VolunteerRepository
        var volunteerResult=await _volunteerRepository
            .GetById(VolunteerId.Create(presignedGetObjectArgsDto.volunteerId),cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var pet=volunteerResult.Value.Pets
            .FirstOrDefault(p=>p.Id.Value==presignedGetObjectArgsDto.petId);

        if (pet == null)
            return Errors.General.NotFound(presignedGetObjectArgsDto.petId);

        var photoPathResult = PhotoPath.CreateOfString(petResult.Value);
        var petPhotoResult=PetPhoto.Create(photoPathResult.Value, false);
        var petPhotos=new List<PetPhoto>();
        petPhotos.Add(petPhotoResult.Value);
        var petListPhoto = PetListPhoto.Create(petPhotos);
        
        
        pet.UpdateFilePhotosList(petListPhoto.Value);

        return pet;
    }
}
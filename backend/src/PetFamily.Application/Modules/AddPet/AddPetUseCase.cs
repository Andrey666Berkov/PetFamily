using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.AddPet;
public class AddPetUseCase
{
    private const string BUCKET_NAME="photos";
    private readonly IPhotosProvider _photosProvider;
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetUseCase(
        IPhotosProvider photosProvider,
        IVolunteerRepository volunteerRepository)
    {
        _photosProvider = photosProvider;
        _volunteerRepository = volunteerRepository;
    }

    //method 
    public async Task<Result<Guid, Error>> ProviderUseCase(
        FileDataDtoCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(VolunteerId.Create(command.VolId));
        
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;

        List<PetPhoto> photos = [];
        List<StreamDataDto> streamsDataDto = [];
        //MINO LOAD
        foreach (var photo in command.Photos)
        {
            var extension = Path.GetExtension(photo.FileName);
            
            var photoPathId = Guid.NewGuid();
            
            var pathStorageResult=PhotoPath.Create(photoPathId, extension);
            if(pathStorageResult.IsFailure)
                return pathStorageResult.Error;
            
            var photoData = new StreamDataDto(
                photo.Stream, 
                pathStorageResult.Value.FullPath);
            
            streamsDataDto.Add(photoData);
            
            var photoPathToStorage=PhotoPath.Create(photoPathId, extension);
            var petPhoto=PetPhoto.Create(photoPathToStorage.Value, false);
            photos.Add(petPhoto.Value);
        }
        var photoDataDto=new PhotoDataDto(streamsDataDto, BUCKET_NAME);
        
        var minioUploadResult=await _photosProvider
            .UploadPhotosAsync(photoDataDto, cancellationToken);
            
            
        if(minioUploadResult.IsFailure)
            return minioUploadResult.Error;

        var petId = PetId.CreateNewPetId();
        var address = Address.Create(
            command.Address.Street, 
            command.Address.City, 
            command.Address.Country).Value;
        
        var  requisite=Requisite.Create(
            command.requisite.Title,
            command.requisite.Description).Value;
        
        var  speciesBreed = SpeciesBreed.Create(
            SpeciesId.CreateEmpty(), 
            BreedId.CreateEmpty().Value).Value;
        
        
        var pet = new Pet(
            petId, 
            command.NickName,
            command.Description,
            PetType.Cat,
            null,
            null,
            address,
            20,
            180,
            89852525,
            false,
            requisite,
            speciesBreed,
            
            new PetListPhoto(photos));

        volunteerResult.Value.AddPet(pet);
        
        var volunteer=await _volunteerRepository
            .Save(volunteerResult.Value, cancellationToken);
       
       
        return pet.Id.Value;
    }
}
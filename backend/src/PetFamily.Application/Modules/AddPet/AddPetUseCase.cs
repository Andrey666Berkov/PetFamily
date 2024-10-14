using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
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
    private const string BUCKET_NAME = "photos";
    private readonly IFilesProvider _filesProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetUseCase> _logger;

    public AddPetUseCase(
        IFilesProvider filesProvider,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetUseCase> logger)
    {
        _filesProvider = filesProvider;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    //method 
    public async Task<Result<Guid, Error>> ProviderUseCase(
        FileDataDtoCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            List<PetPhoto> photos = [];
            List<FileDataDto> streamsDataDto = [];
            //MINO LOAD
            foreach (var photo in command.Photos)
            {
                var extension = Path.GetExtension(photo.FilePath);

                var photoPathId = Guid.NewGuid();

                var pathStorageResult = FilePath.Create(photoPathId, extension);
                if (pathStorageResult.IsFailure)
                    return pathStorageResult.Error;

                var photoData = new FileDataDto(
                    photo.Stream,
                    pathStorageResult.Value,
                    photo.BacketName);

                streamsDataDto.Add(photoData);

                var photoPathToStorage = FilePath.Create(photoPathId, extension);
                var petPhoto = PetPhoto.Create(photoPathToStorage.Value, false);
                photos.Add(petPhoto.Value);
            }

            var photoDataDto = new PhotoDataDto(streamsDataDto, BUCKET_NAME);

            var minioUploadResult = await _filesProvider
                .UploadFilesAsync(streamsDataDto, cancellationToken);


            if (minioUploadResult.IsFailure)
                return minioUploadResult.Error;

            var petId = PetId.CreateNewPetId();
            var address = Address.Create(
                command.Address.Street,
                command.Address.City,
                command.Address.Country).Value;

            var requisite = Requisite.Create(
                command.requisite.Title,
                command.requisite.Description).Value;

            var speciesBreed = SpeciesBreed.Create(
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
                command.Weigth,
                command.Heigth,
                command.NumberPhone,
                command.IsCastrated,
                requisite,
                speciesBreed,
                new PetListPhoto(photos));

            volunteerResult.Value.AddPet(pet);
            await _unitOfWork.SaveChanges(cancellationToken);

            /*var volunteer = await _volunteerRepository
                .Save(volunteerResult.Value, cancellationToken);*/

            transaction.Commit();
            return pet.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Can not add pet to module - {id} in transaction",
                command.VolunteerId);

            transaction.RollBack(cancellationToken);

            return Error.Failure("Can not add pet to module - {id}", "volunteer.pet.failure");
        }
    }
}
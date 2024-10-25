using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.PetManagment.UseCases.GetPet;

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

    public async Task<Result<Pet, Error>> Handler(
        GetPetCommand getPetCommand,
        CancellationToken cancellationToken) 
    {
        //minio
        var petResult= await _filesProvider
            .GetFileAsync(getPetCommand, cancellationToken);
        
        //VolunteerRepository
        var volunteerResult=await _volunteerRepository
            .GetById(VolunteerId.Create(getPetCommand.VolunteerId),cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var pet=volunteerResult.Value.Pets
            .FirstOrDefault(p=>p.Id.Value==getPetCommand.PetId);

        if (pet == null)
            return Errors.General.NotFound(getPetCommand.PetId);

        var photoPathResult = FilePath.CreateOfString(petResult.Value);
        var petPhotorResult = PetFile.Create(photoPathResult.Value, false);
        var listPhot=new List<PetFile>();
        listPhot.Add(petPhotorResult.Value);
        var photos= new ValueObjectList<PetFile>(listPhot);
        
        pet.UpdateFiles(photos);

        return pet;
    }
}
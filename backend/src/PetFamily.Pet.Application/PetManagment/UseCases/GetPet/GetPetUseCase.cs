using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Dtos;
using PetFamily.Core.ValueObjects;
using PetFamily.Pet.Domain.Volunteers.IDs;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.GetPet;

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

    public async Task<Result<Domain.Volunteers.Pet, ErrorMy>> Handler(
        GetPetCommand getPetCommand,
        CancellationToken cancellationToken) 
    {
        //minio
        var commandProvader = new PetCommandProvider( 
            getPetCommand.Bucket,
            getPetCommand.PetId, 
            getPetCommand.VolunteerId);
        
        var petResult= await _filesProvider
            .GetFileAsync(commandProvader, cancellationToken);
        
        //VolunteerRepository
        var volunteerResult=await _volunteerRepository
            .GetById(VolunteerId.Create(getPetCommand.VolunteerId),cancellationToken);
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var pet=volunteerResult.Value.Pets
            .FirstOrDefault(p=>p.Id.Value==getPetCommand.PetId);

        if (pet == null)
            return ErrorsMy.General.NotFound(getPetCommand.PetId);

        var photoPathResult = FilePath.CreateOfString(petResult.Value);
        var petPhotorResult = PetFile.Create(photoPathResult.Value, false);
        var listPhot=new List<PetFile>();
        listPhot.Add(petPhotorResult.Value);
        var photos= new ValueObjectList<PetFile>(listPhot);
        
        pet.UpdateFiles(photos);

        return pet;
    }
}
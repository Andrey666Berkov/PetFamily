using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Modules.Entity;
using PetFamily.Domain.Modules.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.CreateVolunteer;

public class CreateVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerUseCase(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<Result<Guid, Error>> Create(
        CreateVolunteerRequest createVolunteerRequest,
        CancellationToken cancellationToken =default)
    {
        //валидация
        var valunteerNameResult = await _volunteerRepository
            .GetByName(createVolunteerRequest.FirstName);

        if (valunteerNameResult.IsSuccess)
            return Errors.Volunteers.AllReadyExist();
        
        
        
        //создание доменной модели
        Result<Requisite, Error> requisiteResult=Requisite.Create(
            createVolunteerRequest.requisite.Title,
            createVolunteerRequest.requisite.Description);
        
        if(requisiteResult.IsFailure)
            return Result.Failure<Guid, Error>(requisiteResult.Error);

        var listRequisites=ListRequisites.Create(requisiteResult.Value).Value;
       
        
        Result<SocialNetwork, Error> socialNetworkResult=SocialNetwork.Create(
            createVolunteerRequest.socialNetworkRequest.Title,
            createVolunteerRequest.socialNetworkRequest.Description);
        
        if(socialNetworkResult.IsFailure)
            return Result.Failure<Guid, Error>(socialNetworkResult.Error);
        
        var listSocialNetwork=ListSocialNetwork.Create(socialNetworkResult.Value).Value;
        
        var volunteerId = VolunteerId.CreateNew();

        var volunteerResult = Volunteer.CreateVolunteer(
            volunteerId,
            createVolunteerRequest.FirstName,
            createVolunteerRequest.LastName,
            createVolunteerRequest.MiddleName,
            createVolunteerRequest.Email,
            createVolunteerRequest.Description,
            createVolunteerRequest.NumberPhone,
            createVolunteerRequest.Experience,
            listRequisites,
            listSocialNetwork);
        
        
        //сохранение в бд
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
        
        return  Result.Success<Guid, Error>(volunteerResult.Value.Id.Value);
        
    }
}
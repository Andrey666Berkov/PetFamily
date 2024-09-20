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
             //ListRequisites
             
             List<Requisite> requisites=null; 
             
             if (createVolunteerRequest.requisitesDto is not null)
             {
                 requisites=new List<Requisite>();
                 foreach (var req in createVolunteerRequest.requisitesDto)
                 {
                     var reqResult =Requisite.Create(req.Title, req.Description);
                     if (reqResult.IsSuccess)
                              requisites.Add(reqResult.Value);
                 }
             }
        Result<ListRequisites, Error> listRequisitesResult=ListRequisites.Create(requisites);
        
        if(listRequisitesResult.IsFailure)
            return  Result.Failure<Guid, Error>(listRequisitesResult.Error);
        
             //ListsocialNetworkResult
             List<SocialNetwork> socilanetworks=null; 
             
             if (createVolunteerRequest.SocialNetworkDto is not null)
             {
                 socilanetworks=new List<SocialNetwork>();
                 foreach (var soc in createVolunteerRequest.SocialNetworkDto)
                 {
                     var socRezult= SocialNetwork.Create(soc.Name, soc.Link);
                     if (socRezult.IsSuccess)
                         socilanetworks.Add(socRezult.Value);
                 }
             }
        var listSocialNetwork=ListSocialNetwork.Create(socilanetworks);
        
        if(listSocialNetwork.IsFailure)
              return  Result.Failure<Guid, Error>(listRequisitesResult.Error);
             
            //volunter
        var volunteerId = VolunteerId.CreateNew();

        var volunteerResult = Volunteer.Create(
            volunteerId,
            createVolunteerRequest.FirstName,
            createVolunteerRequest.LastName,
            createVolunteerRequest.MiddleName,
            createVolunteerRequest.Email,
            createVolunteerRequest.Description,
            createVolunteerRequest.NumberPhone,
            createVolunteerRequest.Experience,
            listRequisitesResult.Value,
            listSocialNetwork.Value);
        
        if(volunteerResult.IsFailure)
            return Errors.General.ValueIsInavalid("Volunteer");
        
        //сохранение в бд
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);
        
        return  Result.Success<Guid, Error>(volunteerResult.Value.Id.Value);
        
    }
}
using System.Runtime.Intrinsics.X86;
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.CreateVolunteer;

public class CreateVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<CreateVolunteerRequest> _validator;

    public CreateVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerRequest> validator)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Create(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //валидация
        var validationResult= await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            var error=Error.Validation(
                validationResult.Errors[0].ErrorCode, 
                validationResult.Errors[0].ErrorMessage);
            
            return error;
        }
        
        var valunteerNameResult = await _volunteerRepository
            .GetByName(request.FirstName);

        if (valunteerNameResult.IsSuccess)
            return Errors.Volunteers.AllReadyExist();

        //создание доменной модели
        //ListRequisites
        
        
        

        List<Requisite> requisites = null;

        if (request.requisitesDto is not null)
        {
            requisites = new List<Requisite>();
            foreach (var req in request.requisitesDto)
            {
                var reqResult = Requisite.Create(req.Title, req.Description);
                if (reqResult.IsSuccess)
                    requisites.Add(reqResult.Value);
            }
        }

        Result<ListRequisites, Error> listRequisitesResult = ListRequisites.Create(requisites);

        if (listRequisitesResult.IsFailure)
            return Result.Failure<Guid, Error>(listRequisitesResult.Error);

        //ListsocialNetworkResult
        List<SocialNetwork> socilanetworks = null;

        if (request.SocialNetworkDto is not null)
        {
            socilanetworks = new List<SocialNetwork>();
            foreach (var soc in request.SocialNetworkDto)
            {
                var socRezult = SocialNetwork.Create(soc.Name, soc.Link);
                if (socRezult.IsSuccess)
                    socilanetworks.Add(socRezult.Value);
            }
        }

        var listSocialNetwork = ListSocialNetwork.Create(socilanetworks);

        if (listSocialNetwork.IsFailure)
            return Result.Failure<Guid, Error>(listRequisitesResult.Error);

        //volunter
        var volunteerId = VolunteerId.CreateNew();

        var volunteerResult = Volunteer.Create(
            volunteerId,
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.Email,
            request.Description,
            request.NumberPhone,
            request.Experience,
            listRequisitesResult.Value,
            listSocialNetwork.Value);

        if (volunteerResult.IsFailure)
            return Errors.General.ValueIsInavalid("Volunteer");

        //сохранение в бд
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return Result.Success<Guid, Error>(volunteerResult.Value.Id.Value);
    }
}
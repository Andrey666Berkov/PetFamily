using System.Runtime.Intrinsics.X86;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.Modules.UpdateVolunteerMainInfo;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.CreateVolunteer;

public class CreateVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<UpdateVolunteerInfoUseCase> _logger;

    //ctor
    public CreateVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<UpdateVolunteerInfoUseCase> logger
       )
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        
    }
   //method Create
    public async Task<Result<Guid, ErrorList>> Create(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        //валидация
        var commandValidator= await _validator
            .ValidateAsync(command, cancellationToken);
        if (commandValidator.IsValid == false)
            commandValidator.ToErrorList();
        
        var valunteerNameResult = await _volunteerRepository
            .GetByName(command.Initional.FirstName);

        if (valunteerNameResult.IsSuccess)
            return Errors.Volunteers.AllReadyExist().ToErrorList();

        //создание доменной модели
        //ListRequisites
        Result<ListRequisites, Error> listRequisitesResult = ListRequisites.Empty();
        if(command.RequisitesDto is not null)
        {
            var requisites = new List<Requisite>();
            foreach (var requisiteDto in command.RequisitesDto)
            {
                var requisite=Requisite
                    .Create(requisiteDto.Title, requisiteDto.Description);
                requisites.Add(requisite.Value);
            }
            listRequisitesResult =
            ListRequisites.Create(requisites);
        }
       
        //ListsocialNetworkResult
        Result<ListSocialNetwork, Error> socilanetworksResult 
            = ListSocialNetwork.Empty();
        if(command.SocialNetworkDto is not null)
        {
            var socilaNetworks = new List<SocialNetwork>();
            foreach (var socialDto in command.SocialNetworkDto)
            {
                var socilaNetwork=SocialNetwork
                    .Create(socialDto.Name, socialDto.Link);
                socilaNetworks.Add(socilaNetwork.Value);
            }
            socilanetworksResult =
                ListSocialNetwork.Create(socilaNetworks);
        }
        
        //PhoneNumber
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        //Email
        var email = Email.Create(command.Email).Value;

        //volunter
        var volunteerId = VolunteerId.CreateNew();
        
        //initials
        var initials = Initials.Create(
            command.Initional.FirstName, 
            command.Initional.LastName, 
            command.Initional.MiddleName);

        var volunteerResult = Volunteer.Create(
            volunteerId,
            initials.Value,
            email,
            command.Description,
            phoneNumber,
            command.Experience,
            listRequisitesResult.Value,
            socilanetworksResult.Value
            );

        if (volunteerResult.IsFailure)
            return Errors.General.ValueIsInavalid("Volunteer").ToErrorList();

        //сохранение в бд
        await _volunteerRepository
            .Add(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Created volunteer name={ volunteerResult.Value.FirstName} " +
                               "id={volunteerResult.Value.Id.Value}",
          volunteerResult.Value.Initials.FirstName,
          volunteerResult.Value.Id.Value);

        return Result.Success<Guid, ErrorList>(volunteerResult.Value.Id.Value);
    }
}
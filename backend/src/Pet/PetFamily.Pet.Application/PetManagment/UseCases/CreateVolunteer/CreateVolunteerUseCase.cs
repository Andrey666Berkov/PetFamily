using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.Extensions;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Pet.Application.PetManagment.UseCases.CreateVolunteer;

public class CreateVolunteerUseCase : ICommandUSeCase<Guid, CreateVolunteerCommand>
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
    public async Task<Result<Guid, ErrorListMy>> Handler(
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
            return ErrorsMy.General.AllReadyExist().ToErrorList();

        //создание доменной модели
        //ListRequisites
        Result<ListRequisites, ErrorMy> listRequisitesResult = ListRequisites.Empty();
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
        Result<ListSocialNetwork, ErrorMy> socilanetworksResult 
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
            return ErrorsMy.General.ValueIsInavalid("Volunteer").ToErrorList();

        //сохранение в бд
        await _volunteerRepository
            .Add(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Created volunteer name={ volunteerResult.Value.FirstName} " +
                               "id={volunteerResult.Value.Id.Value}",
          volunteerResult.Value.Initials.FirstName,
          volunteerResult.Value.Id.Value);

        return Result.Success<Guid, ErrorListMy>(volunteerResult.Value.Id.Value);
    }
}
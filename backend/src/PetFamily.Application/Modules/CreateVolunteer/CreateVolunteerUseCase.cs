﻿using System.Runtime.Intrinsics.X86;
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
        
        var valunteerNameResult = await _volunteerRepository
            .GetByName(request.FirstName);

        if (valunteerNameResult.IsSuccess)
            return Errors.Volunteers.AllReadyExist();

        //создание доменной модели
        //ListRequisites
        Result<ListRequisites, Error> listRequisitesResult = ListRequisites.Empty();
        if(request.requisitesDto is not null)
        {
            var requisites = new List<Requisite>();
            foreach (var requisiteDto in request.requisitesDto)
            {
                var requisite=Requisite
                    .Create(requisiteDto.Title, requisiteDto.Description);
                requisites.Add(requisite.Value);
            }
            listRequisitesResult =
            ListRequisites.Create(requisites);
        }
        
        //ListsocialNetworkResult
        Result<ListSocialNetwork, Error> socilanetworksResult = ListSocialNetwork.Empty();
        if(request.SocialNetworkDto is not null)
        {
            var socilaNetworks = new List<SocialNetwork>();
            foreach (var socialDto in request.SocialNetworkDto)
            {
                var socilaNetwork=SocialNetwork
                    .Create(socialDto.Name, socialDto.Link);
                socilaNetworks.Add(socilaNetwork.Value);
            }
            socilanetworksResult =
                ListSocialNetwork.Create(socilaNetworks);
        }
        
        //PhoneNumber
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        //Email
        var email = Email.Create(request.Email).Value;

        //volunter
        var volunteerId = VolunteerId.CreateNew();

        var volunteerResult = Volunteer.Create(
            volunteerId,
            request.FirstName,
            request.LastName,
            request.MiddleName,
            email,
            request.Description,
            phoneNumber,
            request.Experience,
            listRequisitesResult.Value,
            socilanetworksResult.Value);

        if (volunteerResult.IsFailure)
            return Errors.General.ValueIsInavalid("Volunteer");

        //сохранение в бд
        await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

        return Result.Success<Guid, Error>(volunteerResult.Value.Id.Value);
    }
}
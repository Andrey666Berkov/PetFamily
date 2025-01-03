﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.Extensions;
using PetFamily.Shared.Core.File;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Pet.Application.PetManagment.UseCases.AddPet;

public class AddPetUseCase : ICommandUSeCase<Guid, FileDataDtoCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly IFilesProvider _filesProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<FileDataDtoCommand> _validator;
    private readonly ILogger<AddPetUseCase> _logger;

    public AddPetUseCase(
        IFilesProvider filesProvider,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IValidator<FileDataDtoCommand> validator,
        ILogger<AddPetUseCase> logger)
    {
        _filesProvider = filesProvider;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    //method 
    public async Task<Result<Guid, ErrorListMy>> Handler(
        FileDataDtoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository
            .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var pet = InitPet(command);

        volunteerResult.Value.AddPet(pet);
        await _unitOfWork.SaveChanges(cancellationToken);

        return pet.Id.Value;
    }

    private Domain.Volunteers.Pet InitPet(FileDataDtoCommand command)
    {
        var petId = PetId.CreateNewPetId();
        var address = Address.Create(
            command.Address.Street,
            command.Address.City,
            command.Address.Country).Value;

        var requisite = Requisite.Create(
            command.requisite.Title,
            command.requisite.Description).Value;

        var speciesBreed = SpeciesBreed.Create(
            SpeciesId.CreateNew().Value,
            BreedId.CreateNew().Value).Value;

        var pet = new Domain.Volunteers.Pet(
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
            null);
        return pet;
    }
}
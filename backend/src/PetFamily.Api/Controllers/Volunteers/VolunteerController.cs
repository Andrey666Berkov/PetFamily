﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Controllers.Volunteers.Requests;
using PetFamily.Api.Extensions;
using PetFamily.Api.Processors;
using PetFamily.Application.Abstractions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;
using PetFamily.Application.PetManagment.UseCases.AddPet;
using PetFamily.Application.PetManagment.UseCases.CreateVolunteer;
using PetFamily.Application.PetManagment.UseCases.DeleteVolunteer;
using PetFamily.Application.PetManagment.UseCases.GetPet;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;
using PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Controllers.Volunteers;

public class VolunteerController : ApplicationController
{

  [HttpPost("{volunteerid:guid}/get-pet/{petid:guid}")]
    public async Task<ActionResult> GetPet(
        [FromRoute] Guid petid,
        [FromRoute] Guid volunteerid,
        [FromServices] GetPetUseCase getPetUseCase,
        CancellationToken cancellationToken = default)
    {
        var bucket = "photos";
        var presignedGetObjectArgsDto =
            new GetPetCommand(bucket, petid, volunteerid);

        var petResult = await getPetUseCase.Handler(
            presignedGetObjectArgsDto,
            cancellationToken);
        if (petResult.IsFailure)
            return petResult.Error.ToResponse();

        return Ok(petResult.Value);
    }

    [HttpPost("{id:guid}/pet/{petId:guid}/files")]
    public async Task<ActionResult> UploadFilesToPet(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFilesPetUseCase useCase,
        CancellationToken cancellationToken = default)
    {
        await using var process = new FormPhotoProcessor();
        var petPhotos = process.Process(files);

        var command = new UploadFilesToPetCommand(id, petId, petPhotos);
        
        var result= await useCase.Handler(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    

    [HttpPost("{valunteerId:guid}/add-pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid valunteerId,
        [FromBody] AddPetRequest request, //Т.к. IFormFile из фромбоди получить не можем
        [FromServices] AddPetUseCase addPetUseCase, 
        CancellationToken cancellationToken = default)
    {
        var address = new AddressDto(
            request.Address.Street,
            request.Address.Country,
            request.Address.City);

        var requisiteDto = new RequisiteDto(
            request.Requisite.Title,
            request.Requisite.Description);

        var path = Guid.NewGuid().ToString();

        var commands = request.CreateCommand(
            valunteerId,
            address,
            requisiteDto);
            

        var providerUseCaseResult = await addPetUseCase.Handler(
            commands,
            cancellationToken);

        if (providerUseCaseResult.IsFailure)
            return providerUseCaseResult.Error.ToResponse();

        return Ok(providerUseCaseResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerUseCase createVolunteerUseCase,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.CreateCommand();
        
        var result = await createVolunteerUseCase
            .Handler(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerInfoUseCase updateVolunteer,
        [FromBody] UpdateVolunteerRequest req,
        [FromServices] IValidator<UpdateVolunteerInfoCommand> validator,
        CancellationToken cancellationToken = default)
    {
        
        var updateCommand = new UpdateVolunteerInfoCommand(id, req.Initials, req.Description);

        var validationResult = await validator.ValidateAsync(updateCommand, cancellationToken);
      

        var result = await updateVolunteer.Handler(updateCommand, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/socialNetwork-info")]
    public async Task<ActionResult> UpdateSocialNetwork(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerSocialNetworkUseCase updateVolunteer,
        [FromBody] UpdateSocialRequest dto,
        [FromServices] IValidator<UpdateSocialNetworkCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateSocialNetworkCommand(id, dto.SocialNetworks);

        var result = await updateVolunteer.Handler(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerUseCase deleteVolunteer,
        [FromServices] IValidator<DeleteVolunteerCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await deleteVolunteer
            .Handler(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
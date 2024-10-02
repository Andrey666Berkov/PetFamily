using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Api.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Modules.AddPet;
using PetFamily.Application.Modules.CreateVolunteer;
using PetFamily.Application.Modules.DeletePet;
using PetFamily.Application.Modules.DeleteVolunteer;
using PetFamily.Application.Modules.GetPet;
using PetFamily.Application.Modules.UpdateVolunteerMainInfo;
using PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

public class VolunteerController : ApplicationController
{
    [HttpDelete("{id:guid}/Pet")]
    public async Task<ActionResult> DeletePet(
        [FromRoute] Guid id,
        [FromServices] DeletePetUseCase deletePetUseCase,
        CancellationToken cancellationToken = default)
    {
        var deleteDataDto = new DeleteDataDto(id, "photos");
        var petdelete = await deletePetUseCase
            .DeleteUseCase(deleteDataDto);
        if (petdelete.IsFailure)
            return petdelete.Error.ToResponse();

        return Ok(petdelete.Value);
    }
    
    [HttpPost("{id:guid}/Pet")]
    public async Task<ActionResult> GetPet(
        [FromRoute] Guid id,
        [FromServices] GetPetUseCase getPetUseCase,
        CancellationToken cancellationToken = default)
    {
        var bucket = "photos";
        var PresignedGetObjectArgsDto = 
            new PresignedGetObjectArgsDto(bucket, id);
        
       var petResult=await getPetUseCase.GetPat(
           PresignedGetObjectArgsDto,
           cancellationToken);
       if (petResult.IsFailure)
           return petResult.Error.ToResponse();
       
       return Ok(petResult.Value);
    }
    
    [HttpPost("Pet")]
    public async Task<ActionResult> AddPet(
        IFormFile file,
        [FromServices] AddPetUseCase addPetUseCase,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var path = Guid.NewGuid().ToString();
        
        var request = new FiledataDtoRequest(stream, "photos",path);
        
        var providerUseCaseResult = await addPetUseCase.ProviderUseCase(
            request, 
            cancellationToken);
        
        if (providerUseCaseResult.IsFailure)
            return providerUseCaseResult.Error.ToResponse();
        
        return Ok(providerUseCaseResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerUseCase createVolunteerUseCase,
        // [FromServices] CreateVolunteerRequestValidator volunteerValidator,
        [FromBody] CreateVolunteerRequest createVolunteerRequest,
        CancellationToken cancellationToken = default)
    {
        /*var validationResult = await volunteerValidator
           .ValidateAsync(createVolunteerRequest, cancellationToken);

      if (!validationResult.IsValid)
       {
           return validationResult.ToValidationErrorResponse();
       }*/
        Result<Guid, Error> result = await createVolunteerUseCase
            .Create(createVolunteerRequest, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerInfoUseCase updateVolunteer,
        [FromBody] UpdateVolunteerInfoDTO dto,
        [FromServices] IValidator<UpdateVolunteerInfoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVolunteerInfoRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErroResponse();
        }

        var result = await updateVolunteer.Update(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/socialNetwork-info")]
    public async Task<ActionResult> UpdateSocialNetwork(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerSocialNetworkUseCase updateVolunteer,
        [FromBody] UpdateSocialNetworkDto dto,
        [FromServices] IValidator<UpdateSocialNetworkRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateSocialNetworkRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErroResponse();
        }

        var result = await updateVolunteer.Update(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerUseCase deleteVolunteer,
        // [FromBody] DeleteVolunteerRequest requestDto,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteVolunteerRequest(id);

        var validationResult = await validator
            .ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErroResponse();
        }

        var result = await deleteVolunteer
            .Delete(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
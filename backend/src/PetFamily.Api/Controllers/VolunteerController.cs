using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Api.Extensions;
using PetFamily.Api.Response;
using PetFamily.Application.Modules.CreateVolunteer;
using PetFamily.Application.Modules.DeleteVolunteer;
using PetFamily.Application.Modules.UpdateVolunteerMainInfo;
using PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;
using PetFamily.Domain;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

public class VolunteerController : ApplicationController
{
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
           return validationResult.ToValidationErroResponse();
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
        var request = new UpdateVolunteerInfoRequest(id, dto );

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
    
    [HttpPatch("{id:guid}/socialnetwork-info")]
    public async Task<ActionResult> UpdateSocialNetwork(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerSocialNetworkUseCase updateVolunteer,
        [FromBody] UpdateSocialNetworkRequest dto,
        [FromServices] IValidator<UpdateVolunteerSocialNetworkValidate> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateSocialNetworkRequest(id, dto.SocialNetworks );
        
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

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErroResponse();
        }

        var result = await deleteVolunteer.Delete(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
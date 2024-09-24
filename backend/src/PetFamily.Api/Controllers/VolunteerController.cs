using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetFamily.Api.Extensions;
using PetFamily.Api.Response;
using PetFamily.Application.Modules.CreateVolunteer;
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
}
using CSharpFunctionalExtensions;
using PetFamily.Api.Extensions;
using PetFamily.Api.Response;
using PetFamily.Application.Modules.CreateVolunteer;
using PetFamily.Domain;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Controllers;
using Microsoft.AspNetCore.Mvc;


public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerUseCase createVolunteerUseCase,
        [FromBody] CreateVolunteerRequest createVolunteerRequest,
        CancellationToken cancellationToken = default)
    {
       Result<Guid, Error> result= await createVolunteerUseCase.Create(createVolunteerRequest, cancellationToken);

       if (result.IsFailure)
           return result.Error.ToResponse();
        
       return Ok(result.Value);   
    }

   
}
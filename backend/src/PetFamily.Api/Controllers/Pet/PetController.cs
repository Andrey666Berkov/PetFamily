using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Controllers.Pet.Requests;
using PetFamily.Api.Response;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

namespace PetFamily.Api.Controllers.Pet;

public class PetController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetPetWithPaginationRequest request,
        [FromServices] GetFilteredPetWhithPaginationUseCase handle,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response=await handle.Handle(query, cancellationToken);
        
        return Ok(response);
    }
}


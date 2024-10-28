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
        var petQuery = request.ToQuery();
        
        var response=await handle.Handle(petQuery, cancellationToken);
        
        return Ok(response);
    }
    
    [HttpGet("dapper")]
    public async Task<ActionResult> GetDapper(
        [FromQuery] GetPetWithPaginationRequest request,
        [FromServices] GetFilteredPetWhithPaginationUseCaseDapper handle,
        CancellationToken cancellationToken = default)
    {
        var petQuery = request.ToQuery();
        
        var response=await handle.Handle(petQuery, cancellationToken);
        
        return Ok(response);
    }
}



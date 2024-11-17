using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Controllers;
using PetFamily.Pet.Application.PetManagment.Queries.GetVolunteerWhithPagination;
using PetFamily.Pet.Controllers.Pet.Requests;

namespace PetFamily.Pet.Controllers.Pet;

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



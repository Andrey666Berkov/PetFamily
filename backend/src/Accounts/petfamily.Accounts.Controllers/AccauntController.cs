using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using petfamily.Accounts.Application.AccountManagment;
using petfamily.Accounts.Controllers.Request;
using PetFamily.Api.Authorization;
using PetFamily.Api.Controllers;
using PetFamily.Api.Extensions;

namespace petfamily.Accounts.Controllers;

public static class Permissions
{
    public static class Pet
    {
        public const string Create ="pet.create";
    }
}
public class AccauntController : ApplicationController
{
    [Permission("pet.create")]
    [HttpGet("create")]
    public async Task<ActionResult> Create()
    {
        return Ok();
    }

    [Authorize]
    //[Permission("pet.update")]
    [HttpGet("update")]
    public async Task<ActionResult> Update()
    {
        return Ok();
    }


    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult> TestUser()
    {
        return Ok();
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handler(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("loginnnn")]
    public async Task<ActionResult> Login(
        [FromBody] LoginUserRequests userRequest,
        [FromServices] LoginUseCase handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handler(userRequest.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
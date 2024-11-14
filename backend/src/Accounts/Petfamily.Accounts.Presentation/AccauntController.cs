using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petfamily.Accounts.Application.AccountManagment;
using Petfamily.Accounts.Application.AccountManagment.Login;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Accounts.Contracts.Interfaces.Requests;
using PetFamily.Core.Authorization;
using PetFamily.Core.Controllers;
using PetFamily.Core.Extensions;

namespace Petfamily.Accounts.Controllers;


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
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
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
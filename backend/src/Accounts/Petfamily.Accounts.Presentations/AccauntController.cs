using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petfamily.Accounts.Application.AccountManagment.Login;
using Petfamily.Accounts.Application.AccountManagment.RefreshToken;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Accounts.Contracts.Requests;
using PetFamily.Shared.Framework;
using PetFamily.Shared.Framework.Authorization;
using Permission = PetFamily.Shared.Framework.Authorization.Permission;

namespace Petfamily.Accounts.Presentations;

public class AccauntController : ApplicationController
{
    [Permission(Permission.Accounts.ReadPet)]
    [Permission(Permission.Pets.CreatePet)]
    [Authorize]
    [HttpGet("create")]
    public async Task<ActionResult> Create()
    {
        return Ok();
    }
    [Permission("pet.update")]
    [Authorize]
    //[Permission("pet.update")]
    [HttpGet("update")]
    public async Task<ActionResult> Update()
    {
        return Ok();
    }


    [Permission(Permission.Pets.CreatePet)]
    [Authorize]
    [HttpPost("user")]
    public async Task<ActionResult> TestUser()
    {
        return Ok("Hello");
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Handle(
            new RegisterUserCommand(request.Email, request.UserName, request.Password),
            cancellationToken);
        
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
        var result = await handler.Handler(
            new LoginCommand(userRequest.Email, userRequest.Password),
            cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        HttpContext.Response.Cookies.Append("refreshToken",result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }
    
    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshTokens(
        [FromServices] RefreshTokenUseCase handler,
        CancellationToken cancellationToken)
    {
        if (!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        };
        
        Guid.TryParse(refreshToken, out var refreshTokenGuid);
        var result = await handler.Handler(
            new RefreshTokenCommand(refreshTokenGuid),
            cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        HttpContext.Response.Cookies.Append("refreshToken",
            result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace PetFamily.Api.Controllers.AccauntsController;

public class AccauntController : ApplicationController
{
    [HttpPost("jwt")]
    public async Task<ActionResult> Login(CancellationToken cancellationToken)
    {
        
    }
}
//
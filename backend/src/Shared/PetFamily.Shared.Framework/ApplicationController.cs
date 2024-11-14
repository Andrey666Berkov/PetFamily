using Microsoft.AspNetCore.Mvc;
using PetFamily.Shared.Core.Controllers;

namespace PetFamily.Shared.Framework;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        return new(envelope);
    }
}
using PetFamily.Api.Response;

namespace PetFamily.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController:ControllerBase
{
  
   
   public override OkObjectResult Ok(object? value)
   {
      var envelope = Envelope.Ok(value);
      return new (envelope);
   }
}
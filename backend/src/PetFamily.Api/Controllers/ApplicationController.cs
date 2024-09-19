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
using CSharpFunctionalExtensions;
using PetFamily.Domain;

namespace PetFamily.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController:Controller
{
    public IActionResult Index(string nickName, string discription, int numberPhoneOwner)
    {
       //Result<Pet> petResult = 
         //   Pet.CreatePet(nickName, discription, numberPhoneOwner:numberPhoneOwner);
        /*if (petResult.IsFailure)
        {
            return BadRequest(petResult.Error);
        }
        
        var res=Save(petResult.Value);
        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }
        */ 
        return Ok();   
    }

    public Result Save(Pet? pet)
    {
        if (pet != null)
        {
            return Result.Success();
        }

        return Result.Failure("pet = null");
    }
}
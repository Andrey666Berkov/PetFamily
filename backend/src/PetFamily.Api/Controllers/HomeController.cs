using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Enum;

namespace PetFamily.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController:Controller
{
    public IActionResult Index(string nickName, string discription, 
        PetType petType, string breed, string color, string infoHelth, 
        string address, double weight, int height,
        int numberPhoneOwner, bool isCastrated, 
        StatusHelper statusHelper,Requisite? requisite )
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
        
        var pet=Pet.CreatePet(nickName,  discription, 
            petType,  breed, 
            color,  infoHelth, 
            address,  weight,
            height,  numberPhoneOwner, 
            isCastrated, statusHelper, requisite );
        
        pet.Value.SetProperty(description:"dsfsf");
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
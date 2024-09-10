using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Modules;

namespace PetFamily.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController:Controller
{
    public IActionResult Index(string nickName, 
        string discription, 
        PetType petType, 
        string breed,
        string color,
        string infoHelth, 
        string address, 
        double weight, 
        int height,
        int numberPhoneOwner, 
        bool isCastrated, 
        StatusHelper statusHelper,
        Requisite? requisite )
    {
       
        var pet=Pet.CreatePet(PetId.CreateNewPetId() 
            ,nickName,  discription, 
            petType,  breed, 
            color,  infoHelth, 
            address,  weight,
            height,  numberPhoneOwner, 
            isCastrated, statusHelper, requisite );
        
        pet.Value.SetProperty(description:"dsfsf");
        return Ok();   
    }

    /*public Result Save(Pet? pet)
    {
        if (pet != null)
        {
            return Result.Success();
        }

        return Result.Failure("pet = null");
    }
    */
}
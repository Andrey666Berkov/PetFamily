using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Modules.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.Entity;

public class Pet : Shared.Entity<PetId>
{
    //constructor
    public Pet(PetId id):base(id)
    {
    }
    private Pet(PetId id, 
        string nickName, 
        string description, 
        PetType petType, 
        string breed, 
        string color,
        string infoHealth, 
        Address address,
        double weight,
        int height, 
        int numberPhoneOwner, 
        bool isCastrated, 
        Requisite requisite, 
        SpeciesBreed speciesBreed):base(id)
    {
        NickName = nickName; 
        Description = description;
        PetType = petType;
        Breed = breed;
        Color = color; 
        InfoHelth = infoHealth;
        Address = address;
        Weight = weight;
        Height = height; 
        NumberPhoneOwner = numberPhoneOwner;
        IsCastrated = isCastrated;
        SpeciesBreed = speciesBreed;
        AddRequisites(requisite);
    }
    /// //////////////////////////////////////
    //property
    public string NickName { get; private set; }= default!;
    public PetType PetType { get; private set; }
    public string Description { get; private set; }= default!;
    public string Breed { get; private set; }
    public string Color { get; private set; }
    public string InfoHelth { get; private set; } 
    public Address Address { get; private set; }
    public double Weight { get; private set; }
    public int Height { get; private set; }
    public int NumberPhoneOwner { get; private set; }
    public SpeciesBreed SpeciesBreed { get; private set; }
    public bool IsCastrated { get; private set; } = false; 
    public DateOnly? BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; } = false;
    public StatusHelper StatusHelper { get; private set; }
    public ListRequisites RequisiteList { get; private set; }
    public DateTime CreatedOn  => DateTime.Now;
    public PetListPhoto? PhotosList { get; private set; }
    /// //////////////////////////
    
    //methods
    public static Result<Pet,Error> CreatePet(PetId petid, 
        string nickName, string description, 
        PetType petType, 
        string breed, 
        string color, 
        string infoHealth, 
        Address address, 
        double weight, 
        int height,
        int numberPhoneOwner, 
        bool isCastrated, 
        StatusHelper statusHelper,
        Requisite? requisite,
        SpeciesBreed speciesBreed)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            return Errors.General.ValueIsInavalid(nameof(nickName));
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInavalid(nameof(description)); 
        
        if (petType < (PetType)0 || petType > (PetType)2 )
            return Errors.General.ValueIsInavalid(nameof(petType)); 
        
        if (string.IsNullOrWhiteSpace(breed))
            return Errors.General.ValueIsInavalid(nameof(breed));
        
        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsInavalid(nameof(color));     
        
        if (string.IsNullOrWhiteSpace(infoHealth))
            return Errors.General.ValueIsInavalid(nameof(infoHealth));        
        
        if (weight < 0.0 && weight > 300.0)
            return Errors.General.ValueIsInavalid(nameof(weight));
        
        if (height < 0.0 && height > 3.0)
            return Errors.General.ValueIsInavalid(nameof(height));        ;
        
        if (statusHelper < (StatusHelper)0 || statusHelper > (StatusHelper)3 )
            return Errors.General.ValueIsInavalid(nameof(statusHelper));        ;
        
        if(numberPhoneOwner.ToString().Length<5 || numberPhoneOwner.ToString().Length>20)
            return Errors.General.ValueIsInavalid(nameof(numberPhoneOwner));        ;
        
        if (requisite == null)
            return Errors.General.ValueIsInavalid(nameof(requisite));        ;
        
        var pet = new Pet( petid, 
            nickName, 
            description, 
            petType, 
            breed, 
            color,  
            infoHealth, 
            address, 
            weight,
            height, 
            numberPhoneOwner, 
            isCastrated,
            requisite,
            speciesBreed);
        
        return Result.Success<Pet, Error>(pet);
    }

    public void UpdateFilePhotosList(PetListPhoto? photosList)
    {
        PhotosList = photosList;
    }
    public void AddRequisites(Requisite requisite)
    {
        RequisiteList.Requisites.Append(requisite);
    }
}
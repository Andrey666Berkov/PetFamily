

using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public sealed class Pet : Entity<PetId>
{
    
    //property
    
    public PetId Id { get; private set; }
    public string NickName { get; private set; }= default!;
    public PetType PetType { get; private set; }
    public string Description { get; private set; }= default!;
    public string Breed { get; private set; }
    public string Color { get; private set; }
    public string InfoHelth { get; private set; } 
    public string Address { get; private set; }
    public double Weight { get; private set; }
    public int Height { get; private set; }
    public int NumberPhoneOwner { get; private set; }
    public bool IsCastrated { get; private set; } = false; 
    public DateOnly? BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; } = false;
    public StatusHelper StatusHelper { get; private set; }
    public ListRequisites Requisites { get; private set; }
    public DateTime CreatedOn  => DateTime.Now;
    public PetListPhoto Photos { get; private set; }

    

    /// //////////////////////////
    //constructor
    public Pet(PetId id):base(id)
    {
        Id = id;
    }
    private Pet(PetId id, string nickName, string description, 
        PetType petType, string breed, 
        string color, string infoHealth, 
        string address, double weight,
        int height, int numberPhoneOwner, 
        bool isCastrated, Requisite requisite):base(id)
    {
        Id= id;
        NickName = nickName; Description = description;
        PetType = petType; Breed = breed;
        Color = color; InfoHelth = infoHealth;
        Address = address; Weight = weight;
        Height = height; NumberPhoneOwner = numberPhoneOwner;
        IsCastrated = isCastrated;
        AddRequisites(requisite);
    }
    /// //////////////////////////////////////
    //CretePet
    public static Result<Pet> CreatePet(PetId petid, string nickName, string discription, 
        PetType petType, string breed, string color, string infoHealth, 
        string address, double weight, int height,
        int numberPhoneOwner, bool isCastrated, 
        StatusHelper statusHelper,Requisite? requisite)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            return Result<Pet>.Failure("NickName is not null or empty");
        if (string.IsNullOrWhiteSpace(discription))
            return Result<Pet>.Failure("Discription is not null or empty");
        if (petType < (PetType)0 || petType > (PetType)2 )
            return Result<Pet>.Failure("PetType does not exist");
        if (string.IsNullOrWhiteSpace(breed))
            return Result<Pet>.Failure("Breed is not null or empty");
        if (string.IsNullOrWhiteSpace(color))
            return Result<Pet>.Failure("Color is not null or empty");
        if (string.IsNullOrWhiteSpace(infoHealth))
            return Result<Pet>.Failure("InfoHelth is not null or empty");
        if (string.IsNullOrWhiteSpace(address))
            return Result<Pet>.Failure("Address is not null or empty");
        if (weight < 0.0 && weight > 300.0)
            return Result<Pet>.Failure("The weight should be in the range from 0 to 300");
        if (height < 0.0 && height > 3.0)
            return Result<Pet>.Failure("The height should be in the range from 0 to 3");
        if (statusHelper < (StatusHelper)0 || statusHelper > (StatusHelper)3 )
            return Result<Pet>.Failure("StatusHelper does not exist");
        if(numberPhoneOwner.ToString().Length<5 || numberPhoneOwner.ToString().Length>20)
            return Result<Pet>.Failure("There is no such numberPhone");
        if (requisite == null)
            return Result<Pet>.Failure("Requisite is not null or empty");
        
        var pet = new Pet( petid, nickName,  discription, 
            petType,  breed, 
            color,  infoHealth, 
            address,  weight,
            height,  numberPhoneOwner, 
            isCastrated, requisite);
        return Result<Pet>.Success(pet);
    }
    /// /////////////////////////////////
    //methods
    public void AddRequisites(Requisite requisite)
    {
        Requisites.Requisites.Add(requisite);
    }
    public string RemoveRequisites(Requisite requisite)
    {
        var req=Requisites.Requisites
            .FirstOrDefault(r=>r.Title==requisite.Title);
        if (req != null)
        {
            Requisites.Requisites.Remove(req);
            return "requisite removed";
        }
        return "requisite not found";
    }

    public void SetProperty(string? nickName= null, string? description= null,
        PetType petType=PetType.Cat, string? breed= null, string? color= null, string? infoHealth= null,
        string? address=null, double weight=default, int height=default,
        int numberPhoneOwner=default, StatusHelper statusHelper= StatusHelper.None,
        Requisite? requisite = null)

    {
        if (!string.IsNullOrWhiteSpace(nickName))
        {NickName = nickName;}
        if (!string.IsNullOrWhiteSpace(description))
        {Description = description;}
        PetType = petType;
        if (!string.IsNullOrWhiteSpace(breed))
        { Breed = breed;}
        if (!string.IsNullOrWhiteSpace(color))
        {Color = color;}
        if (!string.IsNullOrWhiteSpace(infoHealth))
        {InfoHelth = infoHealth;}
        if (!string.IsNullOrWhiteSpace(address))
        {Address = address;}
        if(weight !=0)
        {Weight = weight;}
        if(height !=0)
        {Height = height;}
        if(numberPhoneOwner !=0)
        {NumberPhoneOwner = numberPhoneOwner;}
        
        if(requisite!=null)
        {AddRequisites(requisite);}
    }
}
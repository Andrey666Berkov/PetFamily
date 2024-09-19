using CSharpFunctionalExtensions;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Modules.ValueObjects;

namespace PetFamily.Domain.Modules.Entity;

public class Volunteer: Shared.Entity<VolunteerId> 
{
    //constructor
    private Volunteer(VolunteerId id) : base(id)
    {
    }
    
    private Volunteer(VolunteerId id,
        string firstName, 
        string lastName, 
        string middleName,
        string email, 
        string description,
        int numberPhone, 
        int experience, 
        ListRequisites requisite, 
        ListSocialNetwork socialNetwork):base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;
        Description = description;
        NumberPhone = numberPhone;
        Experience = experience;
        RequisitesList = requisite;
        SocialNetworkList = socialNetwork;
    }
    
    //property
    private readonly List<Pet>  _pets=[]; 
    public string FirstName { get; private set; }= default!;
    public string LastName { get; private set; }= default!;
    public string MiddleName { get; private set; }= default!;
    public string Email { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public int Experience { get; private set; }
    public int NumberPhone { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public ListRequisites? RequisitesList { get; private set; }
    public ListSocialNetwork? SocialNetworkList { get; private set; }
    
    /// //////////////////////////
   
    /// /////////////////////////////////
    //methods
    public void AddRequisites(Requisite requisite)
    {
        RequisitesList.Requisites.Add(requisite);
    }
    public void AddSocialNetworks(SocialNetwork socialNetwork)
    {
        SocialNetworkList.SocialNetworks.Add(socialNetwork);
    }
    public int AddPet(Pet pet)
    {
        _pets.Add(pet); 
        return _pets.Count;
    }
    public int GetNumPets()=>_pets.Count;
    public int GetNumPetNeedHelp()
    {
        var petNeedHelp = 
            Pets.Where(pet => pet.StatusHelper == StatusHelper.NeedHelp);
        return petNeedHelp.Count();
    }
    public int GetNumPetSearchHome()
    {
        var petNeedHelp = 
            Pets.Where(pet => pet.StatusHelper == StatusHelper.SearchHome);
        return petNeedHelp.Count();
    }
    public int GetNumPetFoundHome()
    {
        var petNeedHelp = 
            Pets.Where(pet => pet.StatusHelper == StatusHelper.FoundHome);
        return petNeedHelp.Count();
    }
    
    /// //////////////////////////////////////
    //CreateVolunteer
    public static Result<Volunteer> CreateVolunteer(VolunteerId id,
        string firstName, 
        string lastName,
        string middleName, 
        string email,
        string description,
        int numberPhone, 
        int experience,
        ListRequisites? requisite,
        ListSocialNetwork? socialNetwork)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<Volunteer>("FirstName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<Volunteer>("FirstName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(middleName))
            return Result.Failure<Volunteer>("MiddleName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Description is not null or empty");
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return Result.Failure<Volunteer>("MiddleName is not null or empty");
        
        if(numberPhone.ToString().Length<5 || numberPhone.ToString().Length>20)
            return Result.Failure<Volunteer>("There is no such numberPhone");
        
        if (requisite == null)
            return Result.Failure<Volunteer>("Requisite is not null or empty");
        
        var volunteer = new Volunteer(id,
            firstName, 
            lastName,
            middleName,
            email,
            description,
            numberPhone,
            experience, 
            requisite,
            socialNetwork);
        return Result.Success<Volunteer>(volunteer);
    }
}
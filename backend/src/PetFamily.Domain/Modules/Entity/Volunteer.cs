using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public class Volunteer:Entity<VolunteerId> 
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
        int numberPhone, 
        int experience, 
        Requisite requisite):base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;
        NumberPhone = numberPhone;
        Experience = experience;
        AddRequisites(requisite);
    }
    
    //property
    private readonly List<Pet>  _pets=[]; 
    public VolunteerId Id { get; private set; }
    public string FirstName { get; private set; }= default!;
    public string LastName { get; private set; }= default!;
    public string MiddleName { get; private set; }= default!;
    public string Email { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public int Experience { get; private set; }
    public int NumberPhone { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public ListRequisites Requisites { get; private set; }
    public ListSocialNetwork SocialNetwork { get; private set; }
    
    /// //////////////////////////
   
    /// /////////////////////////////////
    //methods
    public void AddRequisites(Requisite requisite)
    {
       Requisites.Requisites.Add(requisite);
    }
    public void AddSocialNetworks(SocialNetwork socialNetwork)
    {
        SocialNetwork.SocialNetwork.Add(socialNetwork);
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
        int numberPhone, 
        int experience,
        Requisite? requisite)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<Volunteer>.Failure("FirstName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            return Result<Volunteer>.Failure("FirstName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(middleName))
            return Result<Volunteer>.Failure("MiddleName is not null or empty");
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return Result<Volunteer>.Failure("MiddleName is not null or empty");
        
        if(numberPhone.ToString().Length<5 || numberPhone.ToString().Length>20)
            return Result<Volunteer>.Failure("There is no such numberPhone");
        
        if (requisite == null)
            return Result<Volunteer>.Failure("Requisite is not null or empty");
        
        var volunteer = new Volunteer(id,
            firstName, 
            lastName,
            middleName,
            email,
            numberPhone,
            experience, 
            requisite);
        return Result<Volunteer>.Success(volunteer);
    }
}
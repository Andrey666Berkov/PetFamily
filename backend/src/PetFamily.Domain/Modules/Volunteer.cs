using CSharpFunctionalExtensions;
using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static VolunteerId CreateNewPetId()=>new(Guid.NewGuid());
    public static VolunteerId CreateEmptyPetID() => new(Guid.Empty);
    public static VolunteerId Create(Guid id) => new(id);

}
public class Volunteer:Entity<VolunteerId> 
{
    //property
    private readonly List<Pet>  _pets=[]; 
    private readonly List<Requisite>  _requisites=[];   
    private readonly List<SocialNetwork> _socialNetworks=[];
   
    public VolunteerId Id { get; private set; }
    public string FirstName { get; private set; }= default!;
    public string LastName { get; private set; }= default!;
    public string MiddleName { get; private set; }= default!;
    public string Email { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public int Experience { get; private set; }
    public int NumberPhone { get; private set; }

    private IReadOnlyList<Pet> Pets => _pets;
    private IReadOnlyList<Requisite> Requisites=>_requisites;
    private IReadOnlyList<SocialNetwork> SocialNetwork=>_socialNetworks;
    /// //////////////////////////
    //constructor
    private Volunteer(VolunteerId id,string firstName, string lastName, 
        string middleName, string email, 
        int numberPhone, int experience, Requisite requisite):base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;
        NumberPhone = numberPhone;
        Experience = experience;
        AddRequisitesVolunteer(requisite);
        
    }
    /// /////////////////////////////////
    //methods
    public void AddRequisitesVolunteer(Requisite requisite)
    {
        _requisites.Add(requisite);
    }
    public string RemoveRequisitesVolunteer(Requisite requisite)
    {
        var req=_requisites.FirstOrDefault(r=>r.Id==requisite.Id);
        if (req != null)
        {
            _requisites.Remove(requisite);
            return "requisite removed";
        }
        return "requisite not found";
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
    
    public void AddRequisites(Requisite requisite)
    {
        _requisites.Add(requisite);
    }
    
    public string RemoveRequisites(Requisite requisite)
    {
        var req=_requisites.FirstOrDefault(r=>r.Id==requisite.Id);
        if (req != null)
        {
            _requisites.Remove(requisite);
            return "requisite removed";
        }
        return "requisite not found";
    }
    public void SetProperty(string? firstName=null, string? lastName=null, 
        string? middleName=null, string? email=null, 
        int numberPhone=default, int experience=default, Requisite? requisite=null)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {FirstName = firstName;}
        if (!string.IsNullOrWhiteSpace(lastName))
        {LastName = lastName;}
        if (!string.IsNullOrWhiteSpace(middleName))
        {MiddleName = middleName;}
        if (!string.IsNullOrWhiteSpace(email))
        {Email = email;}
        if(numberPhone !=0)
        {NumberPhone = numberPhone;}
        if(experience !=0)
        {Experience = experience;}
        if(requisite!=null)
        {AddRequisites(requisite);}
    }
    /// //////////////////////////////////////
    //CreateVolunteer
    public static Result<Volunteer> CreateVolunteer(VolunteerId id,string firstName, string lastName,
        string middleName, string email,
        int numberPhone, int experience,Requisite? requisite)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<Volunteer>("FirstName is not null or empty");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<Volunteer>("FirstName is not null or empty");
        if (string.IsNullOrWhiteSpace(middleName))
            return Result.Failure<Volunteer>("MiddleName is not null or empty");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return Result.Failure<Volunteer>("MiddleName is not null or empty");
        if(numberPhone.ToString().Length<5 || numberPhone.ToString().Length>20)
            return Result.Failure<Volunteer>("There is no such numberPhone");
        if (requisite == null)
            return Result.Failure<Volunteer>("Requisite is not null or empty");
        
        var volunteer = new Volunteer(id,firstName, lastName,
            middleName, email,
            numberPhone, experience, requisite);
        return Result.Success(volunteer);
    }
    
    

}
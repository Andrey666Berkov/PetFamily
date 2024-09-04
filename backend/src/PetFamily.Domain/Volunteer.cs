using CSharpFunctionalExtensions;
using PetFamily.Domain.Enum;

namespace PetFamily.Domain;

public class Volunteer
{
    //property
    private readonly List<Pet>  _pets; 
    private readonly List<Requisite>  _requisites;   
    private readonly List<SocialNetwork> _socialNetworks;
   
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }= default!;
    public string LastName { get; private set; }= default!;
    public string MiddleName { get; private set; }= default!;
    public string Email { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public int Experience { get; private set; }
    public int NumberPhone { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<Requisite> Requisites=>_requisites;
    public IReadOnlyList<SocialNetwork> SocialNetwork=>_socialNetworks;
    /// //////////////////////////
    //constructor
    private Volunteer(string firstName, string lastName, 
        string middleName, string email, 
        int numberPhone, int experience, Requisite requisite)
    {
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
    public int GetNumPetNeedHelp()
    {
        var petNeedHelp = Pets.Where(pet => pet.StatusHelper == StatusHelper.NeedHelp);
        return petNeedHelp.Count();
    }
    public int GetNumPetSearchHome()
    {
        var petNeedHelp = Pets.Where(pet => pet.StatusHelper == StatusHelper.SearchHome);
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
    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }
    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }
    public void SetMiddleName(string middleName)
    {
        MiddleName = middleName;
    }
    public void SetEmail(string email)
    {
        Email = email;
    }
    public void SetDescription(string description)
    {
        Description = description;
    }
    public void SetHeight(int experience)
    {
        Experience = experience;
    }
    public void SetNumberPhone(int numberPhone)
    {
        NumberPhone = numberPhone;
    }
    
    /// //////////////////////////////////////
    //CreateVolunteer
    public static Result<Volunteer> CreateVolunteer(string firstName, string lastName,
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
        
        var volunteer = new Volunteer(firstName, lastName,
            middleName, email,
            numberPhone, experience, requisite);
        return Result.Success(volunteer);
    }
    
    

}
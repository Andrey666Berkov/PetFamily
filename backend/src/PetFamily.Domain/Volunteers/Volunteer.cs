using System.ComponentModel.Design.Serialization;
using CSharpFunctionalExtensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    //constructor
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer(VolunteerId id,
        Initials initials,
        Email email,
        string description,
        PhoneNumber phoneNumber,
        int experience,
        ListRequisites requisite,
        ListSocialNetwork socialNetwork
    ) : base(id)
    {
        Initials = initials;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        Experience = experience;
        RequisitesList = requisite;
        SocialNetworkList = socialNetwork;
    }

    //property
    private readonly List<Pet> _pets = [];
    private bool _isDeleted = false;

    public Initials Initials { get; set; }

    public Email Email { get; private set; }
    public string Description { get; private set; } = default!;
    public int Experience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public ListRequisites RequisitesList { get; private set; } = null!;
    public ListSocialNetwork SocialNetworkList { get; private set; }

    //methods
    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet == null)
            return Errors.General.NotFound(petId.Value);
        return pet;
    }

    public void UpdateSocialNetwork(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworkList = ListSocialNetwork.Create(socialNetworks).Value;
    }

    public void Delete()
    {
        _isDeleted = true;
        foreach (var pet in _pets)
        {
            pet.Delete();
        }
    }

    public void Restore()
    {
        _isDeleted = false;
        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }

    public void UpdateVolunteerInfo(Initials initials, string description)
    {
        Initials = initials;
        Description = description;
    }

    public void AddRequisites(Requisite requisite)
    {
        RequisitesList.Requisites.Append(requisite);
    }

    public void AddSocialNetworks(SocialNetwork socialNetwork)
    {
        SocialNetworkList.SocialNetworks.Append(socialNetwork);
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = SerialNumber.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return Errors.General.ValueIsInavalid("Invalid serial number");

        pet.SetSerialNumber(serialNumberResult.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> MovieSerialNumberPet(Pet pet, SerialNumber serialNumber)
    {
        var serialNumberResult = SerialNumber.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return Errors.General.ValueIsInavalid("Invalid serial number");

        pet.SetSerialNumber(serialNumberResult.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public int DeletePet(Pet pet)
    {
        _pets.Remove(pet);
        return _pets.Count;
    }

    public int GetNumPets() => _pets.Count;

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

    public UnitResult<Error> AddPet(IEnumerable<Pet> pet)
    {
        _pets.AddRange(pet);
        return Result.Success<Error>();
    }

    /// //////////////////////////////////////
    //CreateVolunteer
    public static Result<Volunteer, Error> Create(VolunteerId id,
        Initials initials,
        Email email,
        string description,
        PhoneNumber numberPhone,
        int experience,
        ListRequisites requisite,
        ListSocialNetwork socialNetwork
    )
    {
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInavalid(nameof(description));

        var volunteer = new Volunteer(
            id,
            initials,
            email,
            description,
            numberPhone,
            experience,
            requisite,
            socialNetwork
        );

        return Result.Success<Volunteer, Error>(volunteer);
    }
}
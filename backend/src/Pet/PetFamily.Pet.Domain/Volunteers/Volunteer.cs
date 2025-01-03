﻿using CSharpFunctionalExtensions;
using PetFamily.Shared.Core.Dtos;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Pet.Domain.Volunteers;

public class Volunteer : EntityMy<VolunteerId>, ISoftDeletable
{
    //constructor
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer(VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        PhoneNumber phoneNumber,
        int experience,
        ListRequisites requisite,
        ListSocialNetwork socialNetwork
    ) : base(id)
    {
        FullName = fullName;
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

    public FullName FullName { get; set; }

    public Email Email { get; private set; }
    public string Description { get; private set; } = default!;
    public int Experience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public ListRequisites RequisitesList { get; private set; } = null!;
    public ListSocialNetwork SocialNetworkList { get; private set; }

    //methods
    public Result<Pet, ErrorMy> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet == null)
            return ErrorsMy.General.NotFound(petId.Value);
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

    public void UpdateVolunteerInfo(FullName fullName, string description)
    {
        FullName = fullName;
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

    public UnitResult<ErrorMy> AddPet(Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return ErrorsMy.General.ValueIsInavalid("Invalid serial number");

        pet.SetPosition(serialNumberResult.Value);

        _pets.Add(pet);
        return Result.Success<ErrorMy>();
    }

    public UnitResult<ErrorMy> MoviePositionPet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;
        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<ErrorMy>();

        var adjustedPositionResult = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPositionResult.IsFailure)
            return adjustedPositionResult.Error;

        newPosition = adjustedPositionResult.Value;

        var reuslt = MoviePetsBetweenPositions(newPosition, currentPosition);
        if (reuslt.IsFailure)
            return reuslt.Error;
        
        pet.MoviePosition(newPosition);
        return Result.Success<ErrorMy>();
    }


    private UnitResult<ErrorMy> MoviePetsBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value <= currentPosition.Value)
        {
            var petsToMovie = _pets
                .Where(i => i.Position.Value >= newPosition.Value
                            && i.Position.Value < currentPosition.Value)
                .ToList();
            foreach (var petToMovie in petsToMovie)
            {
                var result = petToMovie.MoviePositionForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMovie = _pets
                .Where(i => i.Position.Value > currentPosition.Value
                            && i.Position.Value <= newPosition.Value)
                .ToList();

            foreach (var petToMovie in petsToMovie)
            {
                var result = petToMovie.MoviePositionBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        
        
        return Result.Success<ErrorMy>();
       
    }


    private Result<Position, ErrorMy> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
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

    public UnitResult<ErrorMy> AddPet(IEnumerable<Pet> pet)
    {
        _pets.AddRange(pet);
        return Result.Success<ErrorMy>();
    }

    /// //////////////////////////////////////
    //CreateVolunteer
    public static Result<Volunteer, ErrorMy> Create(VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        PhoneNumber numberPhone,
        int experience,
        ListRequisites requisite,
        ListSocialNetwork socialNetwork
    )
    {
        if (string.IsNullOrWhiteSpace(description))
            return ErrorsMy.General.ValueIsInavalid(nameof(description));

        var volunteer = new Volunteer(
            id,
            fullName,
            email,
            description,
            numberPhone,
            experience,
            requisite,
            socialNetwork
        );

        return Result.Success<Volunteer, ErrorMy>(volunteer);
    }
}
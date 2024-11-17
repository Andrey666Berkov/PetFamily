using CSharpFunctionalExtensions;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.ValueObjects;

public record Initials
{
    private Initials(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public string FirstName { get;  } 
    public string LastName { get;  } 
    public string MiddleName { get; }
    
    public static Result<Initials,ErrorMy> Create(
        string firstName, 
        string lastName, 
        string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return ErrorsMy.General.ValueIsInavalid(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            return ErrorsMy.General.ValueIsInavalid(nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(middleName))
            return ErrorsMy.General.ValueIsInavalid(nameof(middleName));
            
        return new Initials(firstName, lastName, middleName);
    }
}
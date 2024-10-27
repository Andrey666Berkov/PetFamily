using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

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
    
    public static Result<Initials, Error> Create(
        string firstName, 
        string lastName, 
        string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsInavalid(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsInavalid(nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(middleName))
            return Errors.General.ValueIsInavalid(nameof(middleName));
            
        return new Initials(firstName, lastName, middleName);
    }
}
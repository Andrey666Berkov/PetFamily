using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public string FirstName { get;  } 
    public string LastName { get;  } 
    public string MiddleName { get; }
    
    public static Result<FullName,ErrorMy> Create(
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
            
        return new FullName(firstName, lastName, middleName);
    }
}
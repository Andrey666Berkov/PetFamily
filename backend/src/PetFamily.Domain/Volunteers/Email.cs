using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Email
{
    
    private const string EMAIL_REGEX = @"/^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$/i";
    
    public string Emaill { get; }

    private Email()
    {
        
    }
    private Email(string email)
    {
        Emaill = email;
    }

    public static Result<Email, Error> Create(string email)
    {
        if (Regex.IsMatch(email, EMAIL_REGEX))
        {
            var regex = new Regex(EMAIL_REGEX);
            var emailReg=regex.Match(email).Value;
            return new Email(emailReg);
        }
        return Errors.General.ValueIsInavalid("Volunteer_Email");
    }
}
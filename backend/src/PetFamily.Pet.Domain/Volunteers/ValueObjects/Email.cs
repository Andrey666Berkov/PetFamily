using CSharpFunctionalExtensions;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.ValueObjects;

public record Email
{
    
    private const string EMAIL_REGEX = @"/\A[^@]+@([^@\.]+\.)+[^@\.]+\z/";
    
    public string Emaill { get; }

    private Email()
    {
        
    }
    private Email(string email)
    {
        Emaill = email;
    }

    public static Result<Email, ErrorMy> Create(string email)
    {
        /*if (Regex.IsMatch(email, EMAIL_REGEX))
        {
            var regex = new Regex(EMAIL_REGEX);
            var emailReg=regex.Match(email).Value;
            return new Email(emailReg);
        }*/
        if (!string.IsNullOrWhiteSpace(email))
            return new Email(email);
        
        return ErrorsMy.General.ValueIsInavalid("Volunteer_Email");
    }
}
using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

public record Email
{
    
    private const string EMAIL_REGEX = @"/\A[^@]+@([^@\.]+\.)+[^@\.]+\z/";
    
    public string Name { get; }

    private Email()
    {
        
    }
    private Email(string email)
    {
        Name = email;
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
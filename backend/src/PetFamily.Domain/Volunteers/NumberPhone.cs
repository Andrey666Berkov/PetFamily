using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record PhoneNumber
{
    private const string NUMBER_REG  =@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
    
    public string Phonenumber { get;  }

    private PhoneNumber()
    {
        
    }
    private PhoneNumber(string numberPhone)
    {
        Phonenumber = numberPhone;
    }

    public static Result<PhoneNumber, Error> Create(string numberPhone)
    {
        /*if (Regex.IsMatch(numberPhone, NUMBER_REG))
        {
            Regex regex = new Regex(NUMBER_REG);
            string phoneNumber = regex.Match(numberPhone).Value;
            return new PhoneNumber(phoneNumber);
        }*/
        if (string.IsNullOrWhiteSpace(numberPhone))
        {
            return Errors.General.ValueIsInavalid("PhoneNumber");
            
        }
        return new PhoneNumber(numberPhone);
    }
}
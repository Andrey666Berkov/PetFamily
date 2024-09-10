using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static PetId CreateNewPetId()=>new PetId(Guid.NewGuid());
    public static PetId CreateEmptyPetID() => new PetId(Guid.Empty);
    public static PetId Create(Guid id) => new(id);

}

// public class ID<T> where T : class
// {
//     public static T CreateNewPetId()=>new T(Guid.NewGuid());
//     public static T CreateEmptyPetID() => new T(Guid.Empty);
// }

public record Address
{
    private Address(string street, string city, string country)
    {
        Street = street;
        City = city;
        Country = country;
    }
    public string Street { get; }
    public string Country { get; }
    public string City { get; }

    public static Result<Address> Create(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street) ||
            string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(country))
            return Result<Address>
                .Failure("Address or city or country cannot be null or empty");
        return Result<Address>.Success(new Address(street, city, country));
    }
    
}
namespace PetFamily.Domain.Modules;

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
        if (string.IsNullOrWhiteSpace(street))
            return Result<Address>.Failure("Address cannot be empty");
        
        if (string.IsNullOrWhiteSpace(city))
            return Result<Address>.Failure("City cannot be empty");
        
        if (string.IsNullOrWhiteSpace(country))
            return Result<Address>.Failure("Country cannot be empty");
            
        return Result<Address>.Success(new Address(street, city, country));
    }
}
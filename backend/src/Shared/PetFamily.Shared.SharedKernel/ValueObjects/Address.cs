using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

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

    public static Result<Address, ErrorMy> Create(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            return ErrorsMy.General.ValueIsInavalid(nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            return ErrorsMy.General.ValueIsInavalid(nameof(city));

        if (string.IsNullOrWhiteSpace(country))
            return ErrorsMy.General.ValueIsInavalid(nameof(country));

        return new Address(street, city, country);
    }
}
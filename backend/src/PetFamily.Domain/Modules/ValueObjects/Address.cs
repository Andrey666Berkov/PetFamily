using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

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

    public static Result<Address, Error> Create(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInavalid(nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInavalid(nameof(city));
        
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsInavalid(nameof(country));
            
        return new Address(street, city, country);
    }
}
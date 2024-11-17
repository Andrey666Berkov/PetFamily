using CSharpFunctionalExtensions;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.ValueObjects;

public record Requisite
{
   private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public string Title { get;  }= default!;
    public string Description { get; }= default!;
    public static Result<Requisite, ErrorMy> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return ErrorsMy.General.ValueIsInavalid("Requisite_title");
        
        if (string.IsNullOrWhiteSpace(description))
            return ErrorsMy.General.ValueIsInavalid("Requisite_description");
        
        return new Requisite(title, description);
    }
}
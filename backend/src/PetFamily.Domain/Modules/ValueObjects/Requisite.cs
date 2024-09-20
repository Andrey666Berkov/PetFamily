using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record Requisite
{
   private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public string Title { get;  }= default!;
    public string Description { get; }= default!;
    public static Result<Requisite, Error> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Errors.General.ValueIsInavalid("Requisire_title");
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInavalid("Requisire_description");
        
        return new Requisite(title, description);
    }
}
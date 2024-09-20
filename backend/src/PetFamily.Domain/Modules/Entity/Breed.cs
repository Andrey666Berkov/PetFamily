using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.Entity;

public class Breed : Shared.Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }
    
    public Breed(BreedId id,
        string name, 
        string description):base(id)
    {
        Name = name;
        Description = description;
    }
    
   public string Name { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    
    public static Result<Breed, Error> Create(BreedId id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInavalid(name);
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInavalid(description);

        var breed=new Breed(id,name,description);
        
        return breed;
    }
}
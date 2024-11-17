using CSharpFunctionalExtensions;
using PetFamily.Pet.Domain.Volunteers.IDs;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.Species;

public class Breed : PetFamily.Core.Entity<BreedId>
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
    
    public static Result<Breed, ErrorMy> Create(BreedId id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ErrorsMy.General.ValueIsInavalid(name);
        
        if (string.IsNullOrWhiteSpace(description))
            return ErrorsMy.General.ValueIsInavalid(description);

        var breed=new Breed(id,name,description);
        
        return breed;
    }
}
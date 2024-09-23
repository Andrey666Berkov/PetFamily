using CSharpFunctionalExtensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    {
    }
    
    private Species(SpeciesId id,
        string name,
        string description) : base(id)
    {
        Name = name;
        Description = description;
    }
    
    private readonly List<Breed> _breeds=[]; 
    
    public string Name { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public IReadOnlyList<Breed> Breeds =>_breeds;

    public static Result<Species, Error> Create(SpeciesId id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInavalid("Species_name");
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInavalid("Species_description");

        var species=new Species(id,name,description);
        
        return Result.Success<Species, Error>(species);
    }
}
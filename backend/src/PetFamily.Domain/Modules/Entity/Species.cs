﻿namespace PetFamily.Domain.Modules;

public class Species : Entity<SpeciesId>
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
    
    public SpeciesId Id { get; private set; }
    public string Name { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    public IReadOnlyList<Breed> Breeds =>_breeds;

    public static Result<Species> Create(SpeciesId id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Species>.Failure("Species name cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result<Species>.Failure("Description name cannot be null or empty.");

        var species=new Species(id,name,description);
        
        return Result<Species>.Success(species);
    }
}
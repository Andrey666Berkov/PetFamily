namespace PetFamily.Domain.Modules;

public class Breed : Entity<BreedId>
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
    
    public BreedId Id { get; private set; }
    public string Name { get; private set; }= default!;
    public string Description { get; private set; }= default!;
    
    public static Result<Breed> Create(BreedId id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Breed>.Failure("Species name cannot be null or empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result<Breed>.Failure("Description name cannot be null or empty.");

        var breed=new Breed(id,name,description);
        
        return Result<Breed>.Success(breed);
    }
}
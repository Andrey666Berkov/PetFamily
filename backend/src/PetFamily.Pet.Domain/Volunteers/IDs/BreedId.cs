namespace PetFamily.Pet.Domain.Volunteers.IDs;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static BreedId CreateNew()=>new BreedId(Guid.NewGuid());
    public static BreedId CreateEmpty() => new BreedId(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);
}
namespace PetFamily.Pet.Domain.Volunteers.IDs;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static SpeciesId CreateNew()=>new SpeciesId(Guid.NewGuid());
    public static SpeciesId CreateEmpty() => new SpeciesId(Guid.Empty);
    public static SpeciesId Create(Guid id) => new(id);
}
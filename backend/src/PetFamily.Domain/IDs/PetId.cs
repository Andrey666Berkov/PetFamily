namespace PetFamily.Domain.IDs;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static PetId CreateNewPetId()=>new PetId(Guid.NewGuid());
    public static PetId CreateEmptyPetID() => new PetId(Guid.Empty);
    public static PetId Create(Guid id) => new(id);
}
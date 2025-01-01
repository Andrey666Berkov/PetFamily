
namespace PetFamily.Shared.SharedKernel.ValueObjects.IDs;

public class VolunteerId
{
  
    
    private VolunteerId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static VolunteerId CreateNew()=>new(Guid.NewGuid());
    public static VolunteerId CreateEmpty() => new(Guid.Empty);
    public static VolunteerId Create(Guid id) => new(id);
}
namespace PetFamily.Domain.Modules;

public record PetListPhoto
{
    public List<PetPhoto> Photos { get; private set; }
    
}
namespace PetFamily.Domain.Modules;

public record PetListPhoto
{
    private PetListPhoto()
    {
    }
    public List<PetPhoto> Photos { get;  }
    
}
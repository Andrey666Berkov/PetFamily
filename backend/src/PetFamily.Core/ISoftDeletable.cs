namespace PetFamily.Core;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}
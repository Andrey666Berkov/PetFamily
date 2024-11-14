namespace PetFamily.Shared.SharedKernel;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}
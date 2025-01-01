namespace PetFamily.Shared.Core.Dtos;

public abstract class EntityMy<Tid>   where Tid : notnull
{
    protected EntityMy(Tid id)
    {
       Id = id;
    }
    public Tid Id { get; private set; }
}
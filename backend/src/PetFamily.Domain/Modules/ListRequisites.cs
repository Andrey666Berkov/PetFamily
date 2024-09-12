namespace PetFamily.Domain.Modules;

public record ListRequisites
{
    private ListRequisites()
    {
        
    }
    public List<Requisite> Requisites{ get;  }
    
    
}
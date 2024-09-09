using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public class Requisite
{
    public Guid Id { get; set; }
    public string Title { get; set; }= default!;
    public string Description { get; set; }= default!;
    public Transaction TransactionMoney { get; set; }
    public int PetId { get; set; }
    public Pet? Pet { get; set; } = null;
    public int VolunteerId { get; set; }
    public Volunteer? Volunteer { get; set; } = null;
}
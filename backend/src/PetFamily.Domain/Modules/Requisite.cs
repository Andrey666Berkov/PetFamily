using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public class Requisite
{
    public Guid Id { get; set; }
    public string Title { get; set; }= default!;
    public string Description { get; set; }= default!;
    public Transaction TransactionMoney { get; set; }
}
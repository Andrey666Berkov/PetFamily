using PetFamily.Shared.SharedKernel.ValueObjects;

namespace Petfamily.Accounts.Domain.DataModels;

public class AdminAccaunt
{
    
    public const string ADMIN = nameof(ADMIN);
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public FullName FullName { get; set; }

    private AdminAccaunt()
    {
    }
    public AdminAccaunt(FullName fullName, User user)
    {
        FullName = fullName;
        Id = Guid.NewGuid();
        User = user;
    }
}
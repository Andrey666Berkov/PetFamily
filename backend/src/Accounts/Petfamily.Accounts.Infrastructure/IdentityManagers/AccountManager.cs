using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure.IdentityManagers;

public class AccountManager(AccountDbContext accountsDbContext)
{
    public async Task CreateAdminAccaunt(AdminAccaunt adminAccaunt)
    {
        await accountsDbContext.AdminAccaunts.AddAsync(adminAccaunt);
        await accountsDbContext.SaveChangesAsync();
    }
}
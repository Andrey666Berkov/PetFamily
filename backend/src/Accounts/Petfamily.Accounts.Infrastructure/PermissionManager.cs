using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure;

public class PermissionManager(AccountDbContext accountsDbContext)
{
    public async Task<Permission?> FindByCode(string code)
    {
        return await accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode);

            if (isPermissionExist)
                return;

            await accountsDbContext.Permissions
                .AddAsync(new Permission { Code = permissionCode });
        }

        await accountsDbContext.SaveChangesAsync();
    }
}
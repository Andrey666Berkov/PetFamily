using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure.IdentityManagers;

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
                continue;

            await accountsDbContext.Permissions
                .AddAsync(new Permission { Code = permissionCode });
        }

        await accountsDbContext.SaveChangesAsync();
    }
    
    public async Task<HashSet<string>> GetUserPermissionCodesAsync(Guid userId)
    {
        var permission= await accountsDbContext.Users
            .Include(c=>c.Roles)
            .Where(u=>u.Id==userId)
            .SelectMany(u=>u.Roles)
            .SelectMany(r=>r.RolePermissions)
            .Select(rp=>rp.Permission.Code)
            .ToListAsync();

        return permission.ToHashSet();
    }
}
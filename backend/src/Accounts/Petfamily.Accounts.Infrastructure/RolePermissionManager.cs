using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure;

public class RolePermissionManager(AccountDbContext accountsDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.Code == permissionCode);

            var rolePermissionExist = await accountsDbContext.RolesPermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);

            if (rolePermissionExist)
                continue;

            accountsDbContext.RolesPermissions.Add(new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permission!.Id
            });
        }

        await accountsDbContext.SaveChangesAsync();
    }
}
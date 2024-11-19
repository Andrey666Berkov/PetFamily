using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure.IdentityManagers;

public class RolePermissionManager(AccountDbContext accountsDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsDbContext.Permissions
                .FirstOrDefaultAsync(p => p.Code == permissionCode);
            
            if(permission == null)
                throw new ApplicationException($"Permission code {permissionCode} is not found");

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
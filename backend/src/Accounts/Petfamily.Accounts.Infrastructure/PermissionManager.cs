using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure;

public class PermissionManager
{
    private readonly AccountDbContext _accountsDbContext;

    public PermissionManager(AccountDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }
    public async Task<Permission?> FindByCode(string code)
    {
        return await _accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task AddRangeIfExist(IEnumerable<string> permissions, CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await _accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (isPermissionExist)
                return;

            await _accountsDbContext.Permissions
                .AddAsync(new Permission { Code = permissionCode }, cancellationToken);
        }

        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<HashSet<string>> GetUserPermissionCodesAsync(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        //return new HashSet<string>();
        var permissions=await _accountsDbContext.Users
            .Include(c => c.Roles)
            .Where(c => c.Id == userId)
            .SelectMany(c => c.Roles)
            .SelectMany(c => c.RolePermissions)
            .Select(c => c.Permission.Code)
            .ToListAsync(cancellationToken);
        
        return permissions.ToHashSet();

    }
}
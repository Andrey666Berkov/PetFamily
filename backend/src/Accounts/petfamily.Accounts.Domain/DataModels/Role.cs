using Microsoft.AspNetCore.Identity;

namespace petfamily.Accounts.Domain.DataModels;

public class Role : IdentityRole<Guid>
{
    
    public List<RolePermission> RolePermissions { get; set; }
}
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Petfamily.Accounts.Domain.DataModels;
using Petfamily.Accounts.Infrastructure.IdentityManagers;
using Petfamily.Accounts.Infrastructure.Options;
using PetFamily.Shared.Core;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace Petfamily.Accounts.Infrastructure.Seeding;

public class AccountsSeederSevices(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    RolePermissionManager rolePermissionManager,
    IOptions<AdminOptions> adminOptions,
    AccountManager accountManager,
    ILogger<AccountsSeederSevices> logger)
{
    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync(JsonFilesPath.AccountsSeeder);
        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)!;

        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermissions(seedData);
        // /////////////////////////////////

        var userCheck = await userManager.Users.AnyAsync(u=>u.UserName == adminOptions.Value.UserName);
        if (userCheck==false)
        {
            var adminRole = await roleManager.FindByNameAsync(AdminAccaunt.ADMIN)
                            ?? throw new ApplicationException($"Role not find admin role");

           
            var adminUser = User.CreateAdmin(
                adminOptions.Value.UserName,
                adminOptions.Value.Email, adminRole,
                adminOptions.Value.Password);
           
            
            var fullName = FullName
                .Create(
                    adminOptions.Value.UserName,
                    adminOptions.Value.UserName,
                    adminOptions.Value.UserName)
                .Value;
            adminUser.PasswordHash=new PasswordHasher<User>().HashPassword(adminUser, adminOptions.Value.Password);
            var adminAccaunt = new AdminAccaunt(fullName, adminUser);
            

            await accountManager.CreateAdminAccaunt(adminAccaunt);
            
            
            
        }
    }

    private async Task SeedRolePermissions(RolePermissionOptions seedData)
    {
        var rolePermissions = seedData.Roles.Keys;

        foreach (var roleName in rolePermissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            await rolePermissionManager.AddRangeIfExist(role!.Id, seedData.Roles[roleName]);
        }

        logger.LogInformation($"RolePermissions added to database.");
    }

    private async Task SeedRoles(RolePermissionOptions seedData)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);
            if (existingRole is null)
            {
                await roleManager.CreateAsync(new Role() { Name = role });
            }
        }

        logger.LogInformation($"Roles added to database.");
    }

    private async Task SeedPermissions(RolePermissionOptions seedData)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup =>
            permissionGroup.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);

        logger.LogInformation($"Permissions added to database.");
    }
}
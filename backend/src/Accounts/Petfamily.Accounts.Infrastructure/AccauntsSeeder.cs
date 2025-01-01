using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Petfamily.Accounts.Infrastructure;

public class AccauntsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AccauntsSeeder> _logger;

    public AccauntsSeeder(IServiceScopeFactory serviceScopeFactory,
        ILogger<AccauntsSeeder> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync(JsonFilesPath.AccountsSeeder);

        using var scope = _serviceScopeFactory.CreateScope();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<Role>>();
        var permissionManager = scope.ServiceProvider
            .GetRequiredService<PermissionManager>();
        var rolePpermissionManager = scope.ServiceProvider
            .GetRequiredService<RolePermissionManager>();
        var accountDbContext = scope.ServiceProvider
            .GetRequiredService<AccountDbContext>();

        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)!;

        await SeedPermissions(seedData, permissionManager);

        await SeedRoles(seedData, roleManager);
        // /////////////////////////////////

        await SeedRolePermissions(seedData, roleManager, rolePpermissionManager);
    }

    private async Task SeedRolePermissions(RolePermissionConfig seedData, RoleManager<Role> roleManager,
        RolePermissionManager rolePpermissionManager)
    {
        var rolePermissions = seedData.Roles.Keys;

        foreach (var roleName in rolePermissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            await rolePpermissionManager.AddRangeIfExist(role!.Id, seedData.Roles[roleName]);
        }
        _logger.LogInformation($"RolePermissions added to database.");
    }

    private async Task SeedRoles(RolePermissionConfig seedData, RoleManager<Role> roleManager)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);
            if (existingRole is null)
            {
                await roleManager.CreateAsync(new Role() { Name = role });
            }
        }

        _logger.LogInformation($"Roles added to database.");
    }

    private async Task SeedPermissions(RolePermissionConfig seedData, PermissionManager permissionManager)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup =>
            permissionGroup.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);

        _logger.LogInformation($"Permissions added to database.");
    }
}

public class RolePermissionConfig
{
    public Dictionary<string, string[]> Permissions { get; set; } = [];
    public Dictionary<string, string[]> Roles { get; set; } = [];
}
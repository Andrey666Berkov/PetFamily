using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Application.AccountManagment.RefreshToken;
using Petfamily.Accounts.Domain.DataModels;
using Petfamily.Accounts.Infrastructure.IdentityManagers;
using Petfamily.Accounts.Infrastructure.Options;
using Petfamily.Accounts.Infrastructure.Seeding;
using PetFamily.Shared.Core.Options;

namespace Petfamily.Accounts.Infrastructure;

public static class DapandancyInjection
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JWTTokenProvider>();

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));
        
        services.Configure<AdminOptions>(
            configuration.GetSection(AdminOptions.ADMIN));

        services.AddOptions<JwtOptions>();

        services.AddRegisterIdentity();

        services.AddScoped<AccountDbContext>();

        services.AddSingleton<AccauntsSeeder>();
        services.AddSingleton<AccauntsSeed>();
        services.AddScoped<AccountsSeederSevices>();
        
      
        
        return services;
    }
    
    public static IServiceCollection AddRegisterIdentity(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(op => { op.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();
           
        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<AccountManager>();
        services.AddScoped<IRefreshSessionManager,RefreshSessionManager>();
        
        return services;
    }
}
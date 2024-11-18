using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Domain.DataModels;
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

        services.AddOptions<JwtOptions>();

        services.AddRegisterIdentity();

        services.AddScoped<AccountDbContext>();

        services.AddSingleton<AccauntsSeeder>();

        
        
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
        return services;
    }
}
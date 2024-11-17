
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services
            .AddIdentity<User, Role>(op => { op.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AuthorizationDbContext>();

        services.AddScoped<AuthorizationDbContext>();
           
        return services;
    }
}
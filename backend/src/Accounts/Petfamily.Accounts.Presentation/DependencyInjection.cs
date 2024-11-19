using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using Petfamily.Accounts.Infrastructure.Seeding;

namespace Petfamily.Accounts.Controllers;

public static class DapandancyInjection
{
    public static IServiceCollection AddAccountPresentation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountsContract, AccountsContract>();
        return services;
    }
    
    public static IServiceCollection AddRegisterIdentity(
        this IServiceCollection services)
    {
        return services;
    }
}
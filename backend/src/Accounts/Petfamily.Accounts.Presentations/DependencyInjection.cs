using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;

namespace Petfamily.Accounts.Presentations;

public static class DapandancyInjection
{
    public static IServiceCollection AddAccountPresentation(
        this IServiceCollection services)
    {
        services.AddScoped<IAccountsContract, AccountsContract>();
        return services;
    }
}
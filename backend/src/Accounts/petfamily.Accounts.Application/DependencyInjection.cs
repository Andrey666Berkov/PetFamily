using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Shared.Core.Abstractions;

namespace Petfamily.Accounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPetApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandUSeCase<,>), typeof(ICommandUSeCase<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryUSeCase<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}
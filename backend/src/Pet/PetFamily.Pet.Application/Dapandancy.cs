using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Shared.Core.Abstractions;

namespace PetFamily.Pet.Application;

public static class Dapandancy
{
    public static IServiceCollection AddPetApplication(this IServiceCollection services)
    {
        services.AddPetCommands()
            .AddPetQuerys()
            .AddValidatorsFromAssembly(typeof(Dapandancy).Assembly);
        
        return services;
    }
    
    public static IServiceCollection AddPetCommands(this IServiceCollection services)
    {
        var assembly = typeof(Dapandancy).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandUSeCase<,>), typeof(ICommandUSeCase<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
    
    public static IServiceCollection AddPetQuerys(this IServiceCollection services)
    {
        var assembly = typeof(Dapandancy).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryUSeCase<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
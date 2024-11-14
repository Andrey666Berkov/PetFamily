using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Petfamily.Accounts.Application.AccountManagment.Login;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Shared.Core.Abstractions;

namespace Petfamily.Accounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAccauntApplication(this IServiceCollection services)
    {
        services
            .AddAccauntComands()
            .AddAccauntQuerys()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

       // services.AddScoped<RegisterUserHandler>();
      //  services.AddScoped<LoginUseCase>();
        return services;
    }

    public static IServiceCollection AddAccauntComands(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandUSeCase<,>), typeof(ICommandUSeCase<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddAccauntQuerys(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryUSeCase<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());


        return services;
    }
}
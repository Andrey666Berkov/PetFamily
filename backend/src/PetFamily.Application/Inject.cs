using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstractions;
using PetFamily.Application.AccountManagment;
using PetFamily.Application.Database;
using PetFamily.Application.Massaging;
using PetFamily.Application.PetManagment.UseCases.AddPet;
using PetFamily.Application.PetManagment.UseCases.CreateVolunteer;
using PetFamily.Application.PetManagment.UseCases.DeleteVolunteer;
using PetFamily.Application.PetManagment.UseCases.GetPet;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerMainInfo;
using PetFamily.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;
using PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;


namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddQueries()
            .AddComands()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
     

     /* services.AddScoped<GetFilteredPetWhithPaginationUseCase>()
        services.AddScoped<UpdateVolunteerInfoUseCase>() ;
        services.AddScoped<CreateVolunteerUseCase>() ;
        services.AddScoped<DeleteVolunteerUseCase>();
        services.AddScoped<UpdateVolunteerSocialNetworkUseCase>();
        services.AddScoped<AddPetUseCase>();
        services.AddScoped<GetPetUseCase>();
        services.AddScoped<UploadFilesPetUseCase>();
     */

        return services;
    }

    private static IServiceCollection AddComands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandUSeCase<,>), typeof(ICommandUSeCase<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryUSeCase<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}
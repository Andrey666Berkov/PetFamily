using FluentValidation;
using Microsoft.Extensions.DependencyInjection;



using PetFamily.Core.Abstractions;


namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services
          //  .AddQueries()
        //    .AddComands();
           // .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
     

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

    /*private static IServiceCollection AddComands(this IServiceCollection services)
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
    }*/
}
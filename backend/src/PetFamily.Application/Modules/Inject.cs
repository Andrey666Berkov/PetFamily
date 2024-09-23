using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

using PetFamily.Application.Modules.CreateVolunteer;


namespace PetFamily.Application.Modules;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerUseCase>() ;
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
       
        return  services;
    }
}
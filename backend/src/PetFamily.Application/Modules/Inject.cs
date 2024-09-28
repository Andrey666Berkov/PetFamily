using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

using PetFamily.Application.Modules.CreateVolunteer;
using PetFamily.Application.Modules.DeleteVolunteer;
using PetFamily.Application.Modules.UpdateVolunteerMainInfo;
using PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;


namespace PetFamily.Application.Modules;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerUseCase>() ;
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        services.AddScoped<UpdateVolunteerInfoUseCase>() ;
        services.AddScoped<DeleteVolunteerUseCase>();
        services.AddScoped<UpdateVolunteerSocialNetworkUseCase>();
       
        return  services;
    }
}
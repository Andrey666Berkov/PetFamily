using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Database;
using PetFamily.Application.Modules.AddPet;
using PetFamily.Application.Modules.CreateVolunteer;
using PetFamily.Application.Modules.DeletePet;
using PetFamily.Application.Modules.DeleteVolunteer;
using PetFamily.Application.Modules.GetPet;
using PetFamily.Application.Modules.UpdateVolunteerMainInfo;
using PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerUseCase>() ;
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        services.AddScoped<UpdateVolunteerInfoUseCase>() ;
        services.AddScoped<DeleteVolunteerUseCase>();
        services.AddScoped<UpdateVolunteerSocialNetworkUseCase>();
        services.AddScoped<AddPetUseCase>();
        services.AddScoped<GetPetUseCase>();
        services.AddScoped<DeletePetUseCase>();
        services.AddScoped<IUnitOfWork>();
       
        return  services;
    }
}
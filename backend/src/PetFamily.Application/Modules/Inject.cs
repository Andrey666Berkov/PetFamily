using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Modules;
using PetFamily.Application.Modules.CreateVolunteer;

namespace PetFamily.Application.Modules;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerUseCase>() ;
        return  services;
    }
}
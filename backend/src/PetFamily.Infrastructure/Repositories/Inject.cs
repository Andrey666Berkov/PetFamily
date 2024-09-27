using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Modules;
using PetFamily.Infrastructure.Interseptors;

namespace PetFamily.Infrastructure.Repositories;

public static class Inject
{
    public static IServiceCollection AddInfrostructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>() ;
        
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        return  services;
    }
}
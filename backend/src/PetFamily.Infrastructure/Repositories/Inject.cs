﻿using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Modules;

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
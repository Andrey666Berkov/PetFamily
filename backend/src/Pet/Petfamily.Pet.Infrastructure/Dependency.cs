using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Pet.Application;
using PetFamily.Pet.Application.FileProvider;
using PetFamily.Pet.Application.PetManagment;
using Petfamily.Pet.Infrastructure.DbContexts;
using Petfamily.Pet.Infrastructure.Files;
using Petfamily.Pet.Infrastructure.MessageQuaeues;
using Petfamily.Pet.Infrastructure.Options;
using Petfamily.Pet.Infrastructure.Providers;
using Petfamily.Pet.Infrastructure.Repositories;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.File;
using PetFamily.Shared.Core.Massaging;
using Minio;
using Petfamily.Pet.Infrastructure.BackgroundServices;

namespace Petfamily.Pet.Infrastructure;

public static class Dependency
{
    public static IServiceCollection AddPetInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddFilesCleanerServoces()
            .AddBackGroundServices()
            .AddRepositoryServices()
            .AddMesagesQueque()
            .AddMinio(configuration);

        return services;
    }

    private static IServiceCollection AddMesagesQueque(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueque<IEnumerable<FileInfo>>, 
            InMemoryMesagesQueque<IEnumerable<FileInfo>>>();

        return services;
    }

    private static IServiceCollection AddRepositoryServices(
        this IServiceCollection services)
    {
         services.AddScoped<IVolunteerRepository, VolunteerRepository>();
         services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        return services;
    }

    private static IServiceCollection AddBackGroundServices(
        this IServiceCollection services)
    {
     
        services.AddHostedService<FilesCleanerBackgroundService>();

        return services;
    }

    private static IServiceCollection AddFilesCleanerServoces(
        this IServiceCollection services)
    {
          services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true; //мапить имена с нижнеми подчеркиваниями

        return services;
    }
    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //for Tpattern
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        
        services.AddMinio(o => 
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO)
                .Get<MinioOptions>() ?? throw new Exception("Missing minio options");

            var endpont = configuration["Minio:Endpoint"];
            o.WithEndpoint(minioOptions.Endpoint);
            o.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            o.WithSSL(minioOptions.WithSSL);
        });

       services.AddScoped<IFilesProvider, MinioProvider>();

        return services;
    }
}
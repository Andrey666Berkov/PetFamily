using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Massaging;
using PetFamily.Application.PetManagment;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Files;
using PetFamily.Infrastructure.MessageQuaeues;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using FileInfo = PetFamily.Application.FileProvider.FileInfo;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
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

    private static IServiceCollection AddMesagesQueque(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueque<IEnumerable<FileInfo>>, InMemoryMesagesQueque<IEnumerable<FileInfo>>>();

        return services;
    }

    private static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }

    private static IServiceCollection AddBackGroundServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();

        return services;
    }

    private static IServiceCollection AddFilesCleanerServoces(this IServiceCollection services)
    {
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Modules;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>() ;
        services.AddMinio(configuration);
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        
        return  services;
    }
    
    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //for Tpattern
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        
        services.AddMinio(o =>
        {
            var minioOptions=configuration.GetSection(MinioOptions.MINIO)
                .Get<MinioOptions>() ?? throw new Exception("Missing minio options");
            
            var endpont=configuration["Minio:Endpoint"];
            o.WithEndpoint(minioOptions.Endpoint);
            o.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            o.WithSSL(minioOptions.WithSSL);
        });
        
        services.AddScoped<IPhotosProvider, MinioProvider>();
        
        return services;
    }
    
    
}
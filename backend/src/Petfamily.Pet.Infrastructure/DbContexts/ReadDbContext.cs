using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Pet.Application;
using PetFamily.Core.Dtos;
using Petfamily.Pet.Infrastructure.Constans;

namespace Petfamily.Pet.Infrastructure.DbContexts;

public class ReadDbContext(
    IConfiguration configuration) : DbContext, IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<PetDto> Pets => Set<PetDto>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constanse.DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
    }
    
    private ILoggerFactory CreateLoggerFactory()=>
        LoggerFactory.Create(builder=>
        {
            builder.AddConsole();
        });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Inject).Assembly, type=>
            type.FullName?.Contains("Configurations.Read")?? false);
       
        
        /*modelBuilder.ApplyConfiguration(new VolunteerDtoConfiguration());
        modelBuilder.ApplyConfiguration(new PetDtoConfiguration());*/
    }
}
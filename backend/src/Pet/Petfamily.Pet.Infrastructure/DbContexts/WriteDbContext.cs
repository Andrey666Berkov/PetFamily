using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Pet.Domain.Volunteers.Species;
using Petfamily.Pet.Infrastructure.Constans;

namespace Petfamily.Pet.Infrastructure.DbContexts;

public class WriteDbContext(
    IConfiguration configuration) : DbContext
{
    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Species> Species { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constanse.DATABASE));
       // optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        //optionsBuilder.AddInterceptors(new SoftDeleteInterseptor());
    }
    private ILoggerFactory CreateLoggerFactory()=>
        LoggerFactory.Create(builder=>
        {
            builder.AddConsole();
        });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Dependency).Assembly, type=>
            type.FullName?.Contains("Configurations.Write")?? false);
        /*modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new BreedConfiguration());
        modelBuilder.ApplyConfiguration(new SpeciesConfiguration());*/
    }
}
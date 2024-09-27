using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;
using PetFamily.Infrastructure.Configuration;
using PetFamily.Infrastructure.Interseptors;

namespace PetFamily.Infrastructure;

public class ApplicationDbContext(
    IConfiguration configuration) : DbContext
{
    
    
    private const string DATABASE = "Database";
    
    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Species> Species { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(new SoftDeleteInterseptor());
    }
    private ILoggerFactory CreateLoggerFactory()=>
        LoggerFactory.Create(builder=>
        {
            builder.AddConsole();
        });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
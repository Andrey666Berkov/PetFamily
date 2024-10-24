using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.Configuration.Read;
using PetFamily.Infrastructure.Constans;

namespace PetFamily.Infrastructure.DbContexts;

public class ReadDbContext(
    IConfiguration configuration) : DbContext, IReadDbContext
{
    public DbSet<VolunteerDto> Volunteers { get;  } = null!;
    public DbSet<PetDto> Pets { get;  } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constanse.DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    private ILoggerFactory CreateLoggerFactory()=>
        LoggerFactory.Create(builder=>
        {
            builder.AddConsole();
        });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VolunteerDtoConfiguration());
        modelBuilder.ApplyConfiguration(new PetDtoConfiguration());
    }
}
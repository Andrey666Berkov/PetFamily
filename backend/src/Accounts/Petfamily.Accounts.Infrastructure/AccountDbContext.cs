using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Infrastructure;

public class AccountDbContext(IConfiguration configuration)
    : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Permission> Permissions  => Set<Permission>();
    public DbSet<RolePermission> RolesPermissions  => Set<RolePermission>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Database"));
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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .ToTable("users");
        
        /*modelBuilder.Entity<User>()
            .Property(u=>u.SocialNetworks)
            .HasConversion(
                s=>JsonSerializer.Serialize(s, JsonSerializerOptions.Default),
                json=>JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!);
                */
        
        modelBuilder.Entity<Role>()
            .ToTable("roles");
        
        modelBuilder.Entity<Permission>()
            .ToTable("permissions");
        
        modelBuilder.Entity<RolePermission>()
            .ToTable("role_permissions");

        modelBuilder.Entity<Permission>()
            .HasIndex(p => p.Code)
            .IsUnique();
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp=>rp.Role)
            .WithMany(r=> r.RolePermissions)
            .HasForeignKey(rp=>rp.RoleId);
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp=>rp.Permission)
            .WithMany()
            .HasForeignKey(rp=>rp.PermissionId);

        modelBuilder.Entity<RolePermission>()
            .HasKey(p => new { p.RoleId, p.PermissionId });
        
        modelBuilder.Entity<IdentityUserClaim<Guid>>()
           .ToTable("user_claims");
        
        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");
        
        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");
        
        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");
        
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_role");

        modelBuilder.HasDefaultSchema("accauntss");
    }
}


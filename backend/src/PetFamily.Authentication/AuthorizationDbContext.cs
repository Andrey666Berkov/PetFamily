﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastructure.Constans;


namespace PetFamily.Authentication;

public class AuthorizationDbContext(IConfiguration configuration)
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    /*
    public DbSet<User> Users  => Set<User>();
    public DbSet<Role> Roles  => Set<Role>();
    */

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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<IdentityUser<Guid>>()
            .ToTable("users");
        
        modelBuilder.Entity<IdentityRole<Guid>>()
            .ToTable("roles");
        
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
        
    
    }
}

//fdasfadsefasfdfsads
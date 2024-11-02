using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Massaging;
using PetFamily.Application.PetManagment;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Files;
using PetFamily.Infrastructure.MessageQuaeues;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using FileInfo = PetFamily.Application.FileProvider.FileInfo;

namespace PetFamily.Authentication;

public static class Inject
{
    public static IServiceCollection AddAuthentication1(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddIdentity<User, Role>();
        services.AddScoped<AuthorizationDbContext>();
        
       // services.AddScoped<AuthorizationDbContext>();
            
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("dafsdasdfdsdsfsdfsdfsdfsdfsdfsdfsdfwefdweewfwefweffdsafasdfasd"))
                };
            });

        services.AddAuthorization();
        return services;
    }

   
   

}
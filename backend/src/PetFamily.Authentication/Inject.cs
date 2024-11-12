using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Application.AccountManagment.DataModels;
using PetFamily.Application.Authorization;


namespace PetFamily.Authentication;

public static class Inject
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JWTTokenProvider>();
        
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));

        services.AddOptions<JwtOptions>();
        

        services
            .AddIdentity<User, Role>(op =>
            {
                op.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();
        
        services.AddScoped<AuthorizationDbContext>();
            
        services.AddAuthentication(op=>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, op =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT)
                    .Get<JwtOptions>() ?? throw new Exception("Missing jwt options");
                
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ClockSkew = TimeSpan.Zero
                    
                };
            });

        services.AddAuthorization(
            /*op =>
        {
            /*op.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireClaim("Role","User")
                .RequireAuthenticatedUser()
                .Build();#1#
            
            op.AddPolicy("Petty", policy =>
            {
                policy.Requirements.Add(new PermissionAttribute("Pet"));
            });
            
            /*op.AddPolicy("RequireAdministratorRole", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "Admin");
                policy.RequireAuthenticatedUser();
            });#1#
        }*/
            );

        services.AddSingleton<IAuthorizationHandler, PermissionRequarementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        
        return services;
    }

   
   

}
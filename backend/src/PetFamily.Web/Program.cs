using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Controllers;
using PetFamily.Pet.Application;
using PetFamily.Pet.Controllers.Pet;
using PetFamily.Pet.Controllers.Volunteers;
using Petfamily.Pet.Infrastructure;
using PetFamily.Shared.Core.Options;
using PetFamily.Web.Middlewares;
using PetFamily.Shared.Framework.Authorization;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder
                     .Configuration
                     .GetConnectionString("Seq") ??
                 throw new ArgumentNullException("Seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AccauntController).Assembly)
    .AddApplicationPart(typeof(VolunteerController).Assembly)
    .AddApplicationPart(typeof(PetController).Assembly);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(v =>
{
    v.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "JWT: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    v.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddSerilog();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequarementHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services
    .AddPetInfrastructure(builder.Configuration)
    .AddAccauntApplication()
    .AddPetApplication()
    .AddSingleton<IAuthorizationHandler, PermissionRequarementHandler>()
    .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, op =>
{
    var jwtOptions = builder.Configuration.GetSection(JwtOptions.JWT)
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

builder.Services.AddAuthorization();
/*
builder.Services.AddFluentValidationAutoValidation(con =>
    con.OverrideDefaultResultFactoryWith<CustomResultFactory>());
    */

var app = builder.Build();

app.UseExeptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

   // await app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Presentations;
using Petfamily.Accounts.Infrastructure;
using Petfamily.Accounts.Infrastructure.Seeding;
using PetFamily.Pet.Application;
using PetFamily.Pet.Controllers.Pet;
using PetFamily.Pet.Controllers.Volunteers;
using Petfamily.Pet.Infrastructure;
using PetFamily.Shared.Core.Options;
using PetFamily.Shared.Framework;
using PetFamily.Web.Middlewares;
using PetFamily.Shared.Framework.Authorization;
using Serilog;
using Serilog.Events;
using Sprache;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

var c = builder.Configuration;

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

/*builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();*/

builder.Services
    .AddPetInfrastructure(builder.Configuration)
    .AddAccauntApplication()
    .AddPetApplication()
    .AddAuthorizationInfrastructure(builder.Configuration)
    .AddAccountPresentation()
    .AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>()
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

    op.TokenValidationParameters = TokenValidationParametersFactory
        .CreateWithLifewTime(jwtOptions);
});


builder.Services.AddAuthorization();
/*
builder.Services.AddFluentValidationAutoValidation(con =>
    con.OverrideDefaultResultFactoryWith<CustomResultFactory>());
    */

var app = builder.Build();

var accauntSeeder = app.Services.GetRequiredService<AccauntsSeed>();
await accauntSeeder.SeedAsync();

app.UseExeptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // await app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseCors(config =>
{
    config.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
        
});
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapGet("/api/users", () =>
{
    return Results.BadRequest("Все плохо");
    /*List<string> users = ["users1", "users2", "users3"];
    return Results.Ok(users);*/
});


app.Run();
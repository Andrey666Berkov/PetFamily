using PetFamily.Api.Validations;
using PetFamily.Application.Modules;
using PetFamily.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services
    .AddInfrostructure()
    .AddApplication();

builder.Services.AddFluentValidationAutoValidation(con =>
    con.OverrideDefaultResultFactoryWith<CustomResultFactory>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigration();
 //
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
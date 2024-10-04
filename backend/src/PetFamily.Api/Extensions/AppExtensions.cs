﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure;

namespace PetFamily.Api.Extensions;

public static class AppExtensions
{
    public static async  Task<WebApplication> ApplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
   
        return app;
    }
}
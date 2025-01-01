using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Pet.Infrastructure.Interseptors;

public class SoftDeleteInterseptor:SaveChangesInterceptor
{
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        eventData.Context.ChangeTracker.Entries();
        
        var entries = eventData
            .Context.ChangeTracker.Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
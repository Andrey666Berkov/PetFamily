using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Infrastructure.IdentityManagers;

public class RefreshSessionManager(AccountDbContext accountsDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, ErrorMy>> GetByReffreshToken(Guid refreshToken, CancellationToken cancellationToken)
    {
        var refreshSession = await accountsDbContext.RefreshSessions
            .Include(r=>r.User)
            .FirstOrDefaultAsync(r => r.Id == refreshToken, cancellationToken);

        if (refreshSession is null)
            return ErrorsMy.General.NotFound(refreshToken);

        return refreshSession;
    }
    
    public async void Delete(RefreshSession refreshSession)
    {
        accountsDbContext.RefreshSessions.Remove(refreshSession);
    }
}

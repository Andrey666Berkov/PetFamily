using CSharpFunctionalExtensions;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Application;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, ErrorMy>> GetByReffreshToken(Guid refreshToken, CancellationToken cancellationToken);
    void Delete(RefreshSession refreshSession);
}
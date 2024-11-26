using System.Security.Claims;
using CSharpFunctionalExtensions;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Application;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerationAccessToken(User user, CancellationToken cancellationToken);
    Task<RefreshSession> GeneratedRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken);
    Task<Result<IReadOnlyList<Claim>, ErrorMy>> GetUserClaims(string accessToken, CancellationToken cancellationToken);
}
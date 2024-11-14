using CSharpFunctionalExtensions;
using PetFamily.Accounts.Contracts.Requests;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Accounts.Contracts;

public interface IAccountsContract
{
    Task<UnitResult<ErrorListMy>> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);
}
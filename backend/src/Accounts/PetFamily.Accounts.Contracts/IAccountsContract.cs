namespace PetFamily.Accounts.Contracts;

public interface IAccountsContract
{
    /*
    Task<UnitResult<ErrorListMy>> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);
        */

    Task<HashSet<string>> GetUserPermissionCodesAsync(Guid userId);
}
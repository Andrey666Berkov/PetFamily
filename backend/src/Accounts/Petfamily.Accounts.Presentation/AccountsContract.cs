using CSharpFunctionalExtensions;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Contracts.Requests;
using Petfamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Controllers;

public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;

    public AccountsContract(PermissionManager  permissionManager )
    {
        _permissionManager = permissionManager;
    }
    /*public async Task<UnitResult<ErrorListMy>> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
       
        return await registerUserHandler.Handle(
            new RegisterUserCommand(request.Email,request.UserName, request.Password),
            cancellationToken);
    }*/

    public async Task<HashSet<string>> GetUserPermissionCodesAsync(Guid userId)
    {
        return await _permissionManager.GetUserPermissionCodesAsync(userId);
    }
}
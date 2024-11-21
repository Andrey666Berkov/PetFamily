using PetFamily.Accounts.Contracts;
using Petfamily.Accounts.Infrastructure.IdentityManagers;
using PermissionManager = Petfamily.Accounts.Infrastructure.PermissionManager;

namespace Petfamily.Accounts.Presentations;
public class AccountsContract(PermissionManager permissionManager) : IAccountsContract
{
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
        
        return await permissionManager.GetUserPermissionCodesAsync(userId);
    }
}
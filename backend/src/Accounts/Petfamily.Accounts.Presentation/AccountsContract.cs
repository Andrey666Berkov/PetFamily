using CSharpFunctionalExtensions;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Accounts.Contracts.Interfaces;
using PetFamily.Accounts.Contracts.Interfaces.Requests;
using PetFamily.Core;

namespace Petfamily.Accounts.Controllers;

public class AccountsContract(RegisterUserHandler registerUserHandler) : IAccountsContract
{
    public async Task<UnitResult<ErrorList>> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        return await registerUserHandler.Handle(command, cancellationToken);
    }
}
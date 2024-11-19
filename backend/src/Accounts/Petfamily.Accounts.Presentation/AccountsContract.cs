﻿using CSharpFunctionalExtensions;
using Petfamily.Accounts.Application.AccountManagment.Register;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Contracts.Requests;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Controllers;

public class AccountsContract(RegisterUserHandler registerUserHandler) : IAccountsContract
{
    public async Task<UnitResult<ErrorListMy>> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
       
        return await registerUserHandler.Handle(
            new RegisterUserCommand(request.Email,request.UserName, request.Password),
            cancellationToken);
    }
}
﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Application.AccountManagment.Register;

public class RegisterUserUseCase :ICommandUSeCase<RegisterUserCommand>
{
    private readonly UserManager<User> _usrManager;
    private readonly ILogger<RegisterUserUseCase> _logger;
    
    public RegisterUserUseCase(
        UserManager<User> usrManager,
        RoleManager<Role> roleManager,
        ILogger<RegisterUserUseCase> logger)
    {
        _usrManager = usrManager;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorListMy>>  Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        //проверить что в бд нет пользователя с такой же почтой или логином
        /*
        var existUser= await _usrManager.FindByEmailAsync(command.Email);

        if (existUser != null)
            return Errors.General.AllReadyExist().ToErrorList();
            */

        var user = User.Create(command.UserName, command.Email, command.Password);
        
        var result = await _usrManager.CreateAsync(user, command.Password);
        if (result.Succeeded != false)
        {
            _logger.LogInformation("User created: {userName} successfully", command.UserName);
            return Result.Success<ErrorListMy>();
        }

        _usrManager.AddToRoleAsync(user, "Partisipant");
        
        var errors=result.Errors
            .Select(c => ErrorMy.Failure(c.Code, c.Description))
            .ToList();
         
        return new ErrorListMy(errors);

        //создать в бд пользователя
    }
}
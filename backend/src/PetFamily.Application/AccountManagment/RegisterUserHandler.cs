using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.AccountManagment.DataModels;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.AccountManagment;

public class RegisterUserHandler :ICommandUSeCase<RegisterUserCommand>
{
    private readonly UserManager<User> _usrManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    
    public RegisterUserHandler(
        UserManager<User> usrManager,
        RoleManager<Role> roleManager,
        ILogger<RegisterUserHandler> logger)
    {
        _usrManager = usrManager;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>>  Handler(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        //проверить что в бд нет пользователя с такой же почтой или логином
        /*
        var existUser= await _usrManager.FindByEmailAsync(command.Email);

        if (existUser != null)
            return Errors.General.AllReadyExist().ToErrorList();
            */

        var user = new User
        {
            Email = command.Email,
            UserName = command.UserName,
        };

        var result = await _usrManager.CreateAsync(user, command.Password);
        if (result.Succeeded != false)
        {
            _logger.LogInformation("User created: {userName} successfully", command.UserName);
            return Result.Success<ErrorList>();
        }

        _usrManager.AddToRoleAsync(user, "Partisipant");
         
        
        var errors=result.Errors
            .Select(c => Error.Failure(c.Code, c.Description))
            .ToList();
         
        return new ErrorList(errors);

        //создать в бд пользователя
    }
}
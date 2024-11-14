using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using petfamily.Accounts.Application.Commands;
using petfamily.Accounts.Domain.DataModels;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using ErrorList = PetFamily.Core.ErrorList;

namespace petfamily.Accounts.Application.AccountManagment;

public class LoginUseCase : ICommandUSeCase<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginUseCase> _logger;

    public LoginUseCase(
        UserManager<User> userManager, 
        ITokenProvider tokenProvider,
        ILogger<LoginUseCase> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }
    public async Task<Result<string, ErrorList>> Handler(
        LoginCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user =await _userManager.FindByEmailAsync(command.Email);
        if(user is null)
            return ErrorsMy.General.NotFound().ToErrorList();
        
        var passwordConfird=await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfird == false)
        {
            return ErrorsMy.User.InvalidCredentials().ToErrorList();
        }

        var token= _tokenProvider.GenerationAccessToken(user);
        _logger.LogInformation($"Successfully logged in");
        
        return token;
    }
}
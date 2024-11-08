using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.AccountManagment;
using PetFamily.Application.AccountManagment.DataModels;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Authorization;

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
            return Errors.General.NotFound().ToErrorList();
        
        var passwordConfird=await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfird == false)
        {
            return Errors.User.InvalidCredentials().ToErrorList();
        }

        var token= _tokenProvider.GenerationAccessToken(user);
        _logger.LogInformation($"Successfully logged in");
        
        return token;
    }
}
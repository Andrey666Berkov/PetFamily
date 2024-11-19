using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.SharedKernel;


namespace Petfamily.Accounts.Application.AccountManagment.Login;

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
    public async Task<Result<string, ErrorListMy>> Handler(
        LoginCommand command, 
        CancellationToken cancellationToken = default)
    {
        var c =await _userManager.Users.ToListAsync();
        var user =await _userManager.FindByEmailAsync(command.Email);
        if(user is null)
            return ErrorsMy.General.NotFound().ToErrorList();
        
        /*var passwordConfird=await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfird == false)
        {
            return ErrorsMy.User.InvalidCredentials().ToErrorList();
        }*/

        var token=await _tokenProvider.GenerationAccessToken(user);
        _logger.LogInformation($"Successfully logged in");
        
        return token;
    }
}
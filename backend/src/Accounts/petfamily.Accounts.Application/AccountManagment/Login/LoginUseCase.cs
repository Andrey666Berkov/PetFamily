using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Contracts.Responces;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.SharedKernel;


namespace Petfamily.Accounts.Application.AccountManagment.Login;

public class LoginUseCase : ICommandUSeCase<LoginResponses, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<LoginUseCase> _logger;
    

    public LoginUseCase(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        RoleManager<Role> roleManager,
        ILogger<LoginUseCase> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<Result<LoginResponses, ErrorListMy>> Handler(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return ErrorsMy.General.NotFound().ToErrorList();

        var roles = _roleManager.Roles
            .Where(r=>r.NormalizedName==user.UserName.ToUpper())
            .ToList();
        
        user.SetRoles(roles);
        
        var passwordConfird = await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfird == false)
        {
            return ErrorsMy.User.InvalidCredentials().ToErrorList();
        }

        var accessTokenResult = await _tokenProvider.GenerationAccessToken(user, cancellationToken);
        var refreshToken = await _tokenProvider.GeneratedRefreshToken(user, accessTokenResult.Jti, cancellationToken);
        _logger.LogInformation($"Successfully logged in");

        return new LoginResponses(accessTokenResult.AccessToken, refreshToken.Id);
    }
}
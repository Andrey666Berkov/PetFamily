using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Petfamily.Accounts.Application.AccountManagment.Login;
using PetFamily.Accounts.Contracts.Requests;
using PetFamily.Accounts.Contracts.Responces;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.Models;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Application.AccountManagment.RefreshToken;

public record RefreshTokenCommand(Guid RefreshToken) : ICommands;

public class RefreshTokenUseCase : ICommandUSeCase<LoginResponses, RefreshTokenCommand>
{
    private readonly IRefreshSessionManager _refreshManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RefreshTokenUseCase> _logger;

    public RefreshTokenUseCase(
        IRefreshSessionManager refreshManager,
        ITokenProvider tokenProvider,
        IUnitOfWork unitOfWork,
        ILogger<RefreshTokenUseCase> logger)
    {
        _refreshManager = refreshManager;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LoginResponses, ErrorListMy>> Handler(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        
        //достаем из сессии рефТокен
        var OldRefreshSession = await _refreshManager
            .GetByReffreshToken(command.RefreshToken, cancellationToken);

        
        if (OldRefreshSession.IsFailure)
            OldRefreshSession.Error.ToErrorList();

        //старый рефреш проверяем на протухание
        if (OldRefreshSession.Value.ExpiresIn < DateTime.UtcNow)
        {
            return ErrorsMy.Tokens.ExpiredToken().ToErrorList();
        }
        // поолучить рефреш сессию из бд
        // провалидировать рефреш  токен
        // достать claims из accessToken
        //провадидировать accessToken 
        //сгенерировать новый access и refresh  token

        
        //удаляем старый рефрешсессию
        _refreshManager.Delete(OldRefreshSession.Value);
        await _unitOfWork.SaveChanges(cancellationToken);


        var accessToken=await _tokenProvider
            .GenerationAccessToken(OldRefreshSession.Value.User, cancellationToken);

        var refreshToken = await _tokenProvider
            .GeneratedRefreshToken(OldRefreshSession.Value.User, accessToken.Jti, cancellationToken);

        return new LoginResponses(
            accessToken.AccessToken, 
            refreshToken.Id, 
            OldRefreshSession.Value.UserId,
            OldRefreshSession.Value.User.ToString());

    }
}
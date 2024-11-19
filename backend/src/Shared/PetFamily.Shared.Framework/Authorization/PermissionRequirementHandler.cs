using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using PetFamily.Shared.Core.Models;

namespace PetFamily.Shared.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permission)
    {
        //получить пользователя из клеймов
        //получить пользователя по ID из БД
        //проверить что у пользователя есть нужное разрешение

        using var scope = _serviceScopeFactory.CreateScope();
        var accauntContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();

        /*var userIdstring = context.User.Claims
            .FirstOrDefault(claim => claim.Type == CustomClaims.Id)?.Value;

        if (!Guid.TryParse(userIdstring, out Guid userId))
        {
            context.Fail();
        }*/
        
        var permissions = context.User.Claims
            .Where(claim => claim.Type == CustomClaims.Permission)
             .Select(claim => claim.Value);

        if (permissions.Contains(permission.Code))
        {
            context.Succeed(permission);
            return;
        }

        context.Fail();
    }
}
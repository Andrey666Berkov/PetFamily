using System.Formats.Tar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Shared.Framework.Authorization;



public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
    {
        
    }
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute attribute)
    {
        //получить пользователя из клеймов
        //получить пользователя по ID из БД
        //проверить что у пользователя есть нужное разрешение
        
        var permission = context.User.Claims
            .FirstOrDefault(c => c.Type == attribute.Code);
        if (permission == null)
            return;

        if (permission.Value == attribute.Code)
        {
            context.Succeed(attribute);
        }
        
        context.Succeed(attribute);
    }
}



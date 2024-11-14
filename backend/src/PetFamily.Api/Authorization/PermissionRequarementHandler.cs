using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Api.Authorization;

public class PermissionRequarementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute attribute)
    {
        /*var permission = context.User.Claims
            .FirstOrDefault(c => c.Type =="Permission");
        if (permission == null)
            return;

        if (permission.Value == attribute.Code)
        {
            context.Succeed(attribute);
        }*/
        
        context.Succeed(attribute);
    }
}


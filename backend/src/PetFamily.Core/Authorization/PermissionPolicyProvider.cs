using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Core.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    
    public  Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (string.IsNullOrWhiteSpace(policyName))
            return Task.FromResult<AuthorizationPolicy?>(null);

        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()  //иначе можно зайти не аунтифицированым(проверка на токен)
            .AddRequirements(new PermissionAttribute(policyName))
            .Build();

        return Task.FromResult<AuthorizationPolicy?>(policy);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return  Task.FromResult<AuthorizationPolicy>(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
      return  Task.FromResult<AuthorizationPolicy?>(null);
    }
}
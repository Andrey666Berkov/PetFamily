using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Authentication;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Code { get; }
    
    public PermissionAttribute(string code) : base(policy:code)
    {
        Code = code;
    }
}
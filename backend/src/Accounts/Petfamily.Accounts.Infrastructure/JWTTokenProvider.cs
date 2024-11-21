using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Domain.DataModels;
using Petfamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Shared.Core.Models;
using PetFamily.Shared.Core.Options;

namespace Petfamily.Accounts.Infrastructure;

public class JWTTokenProvider : ITokenProvider
{
    private readonly PermissionManager _permissionManager;
    private readonly JwtOptions _jwtOptions;
    private readonly ITokenProvider _tokenProviderImplementation;

    public JWTTokenProvider(
        IOptions<JwtOptions> options,
        PermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
        _jwtOptions = options.Value;
    }

    public async Task<string> GenerationAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredential=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roleClaims=user.Roles.Select(r=>new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        var permissions =await _permissionManager.GetUserPermissionCodesAsync(user.Id);
        var permissionClaims = permissions.Select(p => new Claim(CustomClaims.Permission, p));
        
        var claims = new List<Claim>
        {
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            
        };
       claims= claims.Concat(roleClaims).Concat(permissionClaims).ToList();
       
        var jwtToken = new JwtSecurityToken(
            issuer:_jwtOptions.Issuer,
            audience:_jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiredMinutesTime),
            signingCredentials: signingCredential,
            claims:claims);
        
       var token=new JwtSecurityTokenHandler().WriteToken(jwtToken);

       return token;
    }
}
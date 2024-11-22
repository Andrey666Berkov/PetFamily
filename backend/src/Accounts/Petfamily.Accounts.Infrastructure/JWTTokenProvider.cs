using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Petfamily.Accounts.Application;
using Petfamily.Accounts.Domain.DataModels;
using PetFamily.Shared.Core.Models;
using PetFamily.Shared.Core.Options;
using PetFamily.Shared.Framework;
using PetFamily.Shared.SharedKernel;

namespace Petfamily.Accounts.Infrastructure;

public class JWTTokenProvider : ITokenProvider
{
    private readonly PermissionManager _permissionManager;
    private readonly JwtOptions _jwtOptions;
    private readonly AccountDbContext _accountDbContext;
    
    public JWTTokenProvider(
        IOptions<JwtOptions> options,
        PermissionManager permissionManager, 
        AccountDbContext dbContext)
    {
        _permissionManager = permissionManager;
        _accountDbContext = dbContext;
        _jwtOptions = options.Value;
    }

    public async Task<JwtTokenResult> GenerationAccessToken(
        User user,
        CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredential=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roleClaims=user.Roles
            .Select(r=>new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        
        var permissions =await _permissionManager
            .GetUserPermissionCodesAsync(user.Id, cancellationToken);
        
        var permissionClaims = permissions
            .Select(p => new Claim(CustomClaims.Permission, p));

        var jti = Guid.NewGuid();
        
        var claims = new List<Claim>
        {
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? ""),
            
        };
       claims= claims.Concat(roleClaims).Concat(permissionClaims).ToList();
       
        var jwtToken = new JwtSecurityToken(
            issuer:_jwtOptions.Issuer,
            audience:_jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiredMinutesTime),
            signingCredentials: signingCredential,
            claims:claims);
        
       var jwtStringToken=new JwtSecurityTokenHandler().WriteToken(jwtToken);

       return new JwtTokenResult(jwtStringToken, jti);
    }

    public async Task<RefreshSession> GeneratedRefreshToken(
        User user, Guid accessTokenJti,  
        CancellationToken cancellationToken)
    {
        user.SetRoles(null);
        var refreshSession = new RefreshSession()
        {
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(30),
            RefreshTokenId = Guid.NewGuid(),
            Jti=accessTokenJti
        };

        _accountDbContext.RefreshSessions.Add(refreshSession);
        await _accountDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession;
    }

    public async Task<Result<IReadOnlyList<Claim>, ErrorMy>> GetUserClaims(string JwtToken,  CancellationToken cancellationToken)
    {
       var jwtHandler=new JwtSecurityTokenHandler();

       var validationTokenParameters = TokenValidationParametersFactory.CreateWithOutLifewTime(_jwtOptions);
       
       var validResult=await jwtHandler.ValidateTokenAsync(JwtToken, validationTokenParameters);

       if (validResult.IsValid == false)
           return ErrorsMy.Tokens.InvalidToken();

       return validResult.ClaimsIdentity.Claims.ToList();

    }
    
    
}
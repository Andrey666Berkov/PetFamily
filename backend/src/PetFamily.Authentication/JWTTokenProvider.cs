using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Application.AccountManagment.DataModels;
using PetFamily.Application.Authorization;
using PetFamily.Infrastructure;

namespace PetFamily.Authentication;

public class JWTTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private ITokenProvider _tokenProviderImplementation;

    public JWTTokenProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerationAccessToken(
        User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredential=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email ?? ""),
            new Claim("Permission", "Pet")
        };
        
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
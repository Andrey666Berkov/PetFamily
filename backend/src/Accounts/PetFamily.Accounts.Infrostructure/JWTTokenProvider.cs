using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using petfamily.Accounts.Application;
using petfamily.Accounts.Domain.DataModels;

namespace PetFamily.Accounts.Infrostructure;

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
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim("Permission", "pet.create")
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
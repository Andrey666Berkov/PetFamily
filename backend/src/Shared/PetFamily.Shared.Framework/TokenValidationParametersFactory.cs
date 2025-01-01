using System.Text;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Shared.Core.Options;

namespace PetFamily.Shared.Framework;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateWithLifewTime(JwtOptions jwtOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }
    public static TokenValidationParameters CreateWithOutLifewTime(JwtOptions jwtOptions)
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }
}
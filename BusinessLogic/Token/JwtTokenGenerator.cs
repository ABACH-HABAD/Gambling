using DataBaseClasses.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Token;

public class JwtTokenGenerator(JwtSettings settings) : IJwtTokenGenerator
{
    public string GenerateAccessJwtToken(User user)
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, (user.Email ?? 
            throw new Exception("У пользователя должна быть указана электронная почта")).ToString()),
            new Claim(ClaimTypes.Role, user.Status.ToString())
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(settings.SecretKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshJwtToken()
    {
        byte[] randomBytes = new byte[64];
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}

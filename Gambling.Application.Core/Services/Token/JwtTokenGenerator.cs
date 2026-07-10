using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Token;

namespace Gambling.Application.Core.Services.Token;

public class JwtTokenGenerator(JwtSettings settings) : IJwtTokenGenerator
{
    public string GenerateAccessJwtToken(UserModel user)
    {
        if (user.Status == null) throw new ArgumentException("У пользователя должен быть задан статус", nameof(user));
        if (user.Email == null) throw new ArgumentException("У пользователя должна быть указана электронная почта", nameof(user));

        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Status.Id.ToString())
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
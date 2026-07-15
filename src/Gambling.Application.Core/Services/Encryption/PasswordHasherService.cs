using System.Security.Cryptography;
using System.Text;
using Gambling.Application.Core.Abstractions.Encryptions;

namespace Gambling.Application.Core.Services.Encryption;

public class PasswordHasherService : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
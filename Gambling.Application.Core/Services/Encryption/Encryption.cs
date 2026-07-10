using Gambling.Application.Core.Abstractions.Encryptions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gambling.Application.Core.Services.Encryption;

public class Encryption : IEncryption, IPasswordHasher, IUserIdExtraction
{
    public string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public int? ExtractUserId(Claim userIdClaim)
    {
        if (userIdClaim is null) return null;
        return int.Parse(userIdClaim.Value);
    }

    public byte[] Encrypt(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        byte[] encryptedBytes = ProtectedData.Protect(bytes, optionalEntropy: null, scope: DataProtectionScope.CurrentUser);

        return encryptedBytes;
    }

    public string Decrypt(byte[] bytes)
    {
        byte[] decryptedBytes = ProtectedData.Unprotect(bytes, optionalEntropy: null, scope: DataProtectionScope.CurrentUser);
        string data = Encoding.UTF8.GetString(decryptedBytes);

        return data;
    }
}
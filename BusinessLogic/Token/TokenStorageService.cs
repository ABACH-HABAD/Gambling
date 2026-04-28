using BusinessLogic.Encryption;
using System.Security.Cryptography;

namespace BusinessLogic.Token;

public abstract class TokenStorageService : ITokenStorageService
{
    protected readonly IEncryption _encryption;
    protected readonly string _tokenFilePath;
    protected string? _cachedToken;

    public TokenStorageService(string file, IEncryption encryption)
    {
        _encryption = encryption;

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Gambling");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        _tokenFilePath = Path.Combine(path, file);
    }

    public async Task SaveTokenAsync(string token)
    {
        await ClearTokenAsync();

        if (token == null || token == string.Empty) return;

        byte[] encryptedBytes = _encryption.Encrypt(token);

        await File.WriteAllBytesAsync(_tokenFilePath, encryptedBytes);
        _cachedToken = token;
    }

    public async Task<string?> GetTokenAsync()
    {
        if (_cachedToken != null) return _cachedToken;

        if (File.Exists(_tokenFilePath))
        {
            try
            {
                byte[] encryptedBytes = await File.ReadAllBytesAsync(_tokenFilePath);
                string token = _encryption.Decrypt(encryptedBytes);

                _cachedToken = token;
                return token;
            }
            catch (CryptographicException)
            {
                await ClearTokenAsync();
                return null;
            }

        }
        else return null;
    }

    public async Task ClearTokenAsync()
    {
        if (File.Exists(_tokenFilePath)) File.Delete(_tokenFilePath);
        _cachedToken = null;
    }
}

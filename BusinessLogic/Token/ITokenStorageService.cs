using System.Security.Cryptography;

namespace BusinessLogic.Token;

public interface ITokenStorageService
{
    public Task SaveTokenAsync(string token);

    public Task<string?> GetTokenAsync();

    public Task ClearTokenAsync();
}

using System.Security.Cryptography;

namespace Gambling.Application.Core.Abstractions.Token;

public interface ITokenStorageService
{
    public Task SaveTokenAsync(string token);

    public Task<string?> GetTokenAsync();

    public Task ClearTokenAsync();
}

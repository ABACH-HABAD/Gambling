using Gambling.Application.Core.Api.Results;

namespace Gambling.Application.Core.Abstractions.Auth;

public interface ILoginChecker
{
    public Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip);
}

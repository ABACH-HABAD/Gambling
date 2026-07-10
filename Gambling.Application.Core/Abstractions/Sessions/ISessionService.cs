using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Services.Token;
using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Sessions;

public interface ISessionService
{
    public Task<SessionModel?> GetSessionAsync(string refreshToken, string? ip);

    public Task Login(UserModel user, string refreshToken, int deviceType, string? ip);

    public Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip);

    public Task<RefreshedTokens?> RefreshTokenAsync(string refreshToken, int deviceType, string? ip);

    public Task Logout(string refreshedToken, int deviceType, string? ip);
}
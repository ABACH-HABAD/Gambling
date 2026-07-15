using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Services.Token;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;

namespace Gambling.Application.Server.Services.Account.Auth.Sessions;

public class ServerSessionService(
    IJwtTokenGenerator jwtTokenGenerator,
    ISessionRepository sessionRepository,
    IUserRepository userRepository)
    : ISessionService, ILoginChecker, ISessionStorageService
{
    public async Task<SessionModel?> GetSessionAsync(string refreshToken, string? ip)
    {
        return (await sessionRepository.FindByRefreshTokenAsync(refreshToken, ip)).session;
    }

    public async Task<List<SessionModel>> GetAllSessionsAsync()
    {
        return await sessionRepository.GetAllSessionsAsync();
    }

    private async Task<UserModel?> GetUserAsync(string refreshToken, int deviceType, string? ip)
    {
        (bool status, int userId) = await sessionRepository.CheckActiveSessionAsync(refreshToken, deviceType, ip);
        if (status)
        {
            UserModel? user = await userRepository.GetWithIdAsync(userId);
            return user;
        }
        else return null;
    }

    public async Task Login(UserModel user, string refreshToken, int deviceType, string? ip)
    {
        await sessionRepository.RecordLoginAsync(user, deviceType, refreshToken, ip);
    }

    public async Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip)
    {
        UserModel? user = await GetUserAsync(refreshToken, deviceType, ip);

        if (user != null) return new LoginResult(new(jwtTokenGenerator.GenerateAccessJwtToken(user), refreshToken), true, "Активаня сессия найдена");
        else return new LoginResult(null, false, "Активаня сессия не найдена");
    }

    public async Task<RefreshedTokens?> RefreshTokenAsync(string refreshToken, int deviceType, string? ip)
    {
        UserModel? user = await GetUserAsync(refreshToken, deviceType, ip);

        if (user != null)
        {
            await Logout(refreshToken, deviceType, ip);
            await Login(user, refreshToken, deviceType, ip);

            return new RefreshedTokens
                (
                AccessToken: jwtTokenGenerator.GenerateAccessJwtToken(user),
                RefreshToken: jwtTokenGenerator.GenerateRefreshJwtToken()
                );
        }
        else return null;
    }

    public async Task Logout(string refreshedToken, int deviceType, string? ip)
    {
        await sessionRepository.CompleteSessionAsync(refreshedToken, deviceType, ip);
    }
}
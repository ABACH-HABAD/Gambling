using BusinessLogic.Token;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Auth;

public class ServerSessionService(IJwtTokenGenerator jwtTokenGenerator, ISessionRepository sessionRepository, IUserRepository userRepository) : ISessionService, ILoginChecker
{
    public async Task<Session?> GetSessionAsync(string refreshToken, string? ip)
    {
        return sessionRepository.FindByRefreshToken(refreshToken, ip);
    }

    private User? GetUser(string refreshToken, int deviceType, string? ip)
    {
        (bool status, int userId) = sessionRepository.CheckActiveSession(refreshToken, deviceType, ip);
        if (status)
        {
            User? user = userRepository.GetWithId(userId);
            return user;
        }
        else return null;
    }

    public async Task Login(User user, string refreshToken, int deviceType, string? ip)
    {
        sessionRepository.RecordLogin(user, deviceType, refreshToken, ip);
    }

    public async Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip)
    {
        User? user = GetUser(refreshToken, deviceType, ip);

        if (user != null) return new LoginResult(new(jwtTokenGenerator.GenerateAccessJwtToken(user), refreshToken), true, "Активаня сессия найдена");
        else return new LoginResult(null, false, "Активаня сессия не найдена");
    }

    public async Task<RefreshedTokens?> RefreshTokenAsync(string refreshToken, int deviceType, string? ip)
    {
        User? user = GetUser(refreshToken, deviceType, ip);

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
        sessionRepository.CompleteSession(refreshedToken, deviceType, ip);
    }
}

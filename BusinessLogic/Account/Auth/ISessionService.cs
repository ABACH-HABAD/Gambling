using BusinessLogic.Token;
using DataBaseClasses.Entity;

namespace BusinessLogic.Account.Auth;

public interface ISessionService
{
    public Task<Session?> GetSessionAsync(string refreshToken, string? ip);

    public Task Login(User user, string refreshToken, int deviceType, string? ip);

    public Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip);

    public Task<RefreshedTokens?> RefreshTokenAsync(string refreshToken, int deviceType, string? ip);

    public Task Logout(string refreshedToken, int deviceType, string? ip);
}

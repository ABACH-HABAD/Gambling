namespace BusinessLogic.Account.Auth;

public interface ILoginChecker
{
    public Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip);
}

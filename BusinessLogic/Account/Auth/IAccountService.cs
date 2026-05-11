using DataBaseClasses.Entity;

namespace BusinessLogic.Account.Auth;

public interface IAccountService
{
    public Task<LoginResult> RegistrateAsync(string login, string hasedPassword, string repeatPassword, DeviceType deviceType, string? ip = null);
    public Task<LoginResult> LoginAsync(string login, string password, DeviceType deviceType, string? ip = null, bool loginAsAdmin = false);
    public Task<LoginResult> AutoLoginAsync(string refreshToken, DeviceType deviceType, string? ip = null);

    public Task<bool> ChangeEmailAsync(int userId, string oldEmail, string newEmail);
    public Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword, string repeatPassword);

    public Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip);
}

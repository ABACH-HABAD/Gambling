using BusinessLogic.Token;
using DataBaseClasses;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository;

namespace BusinessLogic.Auth;

public interface IAccountService
{
    public Task<LoginResult> RegistrateAsync(string login, string hasedPassword, string repeatPassword, DeviceType deviceType, string? ip = null);
    public Task<LoginResult> LoginAsync(string login, string password, DeviceType deviceType, string? ip = null, bool loginAsAdmin = false);
    public Task<LoginResult> AutoLoginAsync(string refreshToken, DeviceType deviceType, string? ip = null);

    public Task<bool> CheckRegistrationAsync(string login);

    public Task<User?> GetUserData(int userId);
    public Task<User?> UpdateUserDataAsync(User user);

    public Task<bool> TopUpBalance(int userId, double sum);
}

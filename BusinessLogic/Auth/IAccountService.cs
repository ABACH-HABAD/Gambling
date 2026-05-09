using DataBaseClasses.Entity;

namespace BusinessLogic.Auth;

public interface IAccountService
{
    public Task<LoginResult> RegistrateAsync(string login, string hasedPassword, string repeatPassword, DeviceType deviceType, string? ip = null);
    public Task<LoginResult> LoginAsync(string login, string password, DeviceType deviceType, string? ip = null, bool loginAsAdmin = false);
    public Task<LoginResult> AutoLoginAsync(string refreshToken, DeviceType deviceType, string? ip = null);

    public Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip);

    //public Task<bool> CheckRegistrationAsync(string login);

    public Task<User?> GetUserDataAsync(int userId);
    public Task<User?> UpdateUserDataAsync(User user);
    public Task<List<User>> GetAllUsersAsync(int adminId);

    public Task<bool> ChangeBalanceAsync(int userId, double sum);
}

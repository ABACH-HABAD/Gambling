using DataBaseClasses.Entity;

namespace BusinessLogic.Account;

public interface IAccountDataService
{
    public Task<User?> GetUserDataAsync(int userId);
    public Task<User?> UpdateUserDataAsync(User user);
    public Task<List<User>> GetAllUsersAsync(int adminId);

    public Task<bool> ChangeBalanceAsync(int userId, double sum);
}
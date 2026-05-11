using BusinessLogic.Account.Balance;
using BusinessLogic.Exceptions;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Account;

public class ServerAccountDataService(IBalanceService balanceService, IUserRepository userRepository) : IAccountDataService
{
    public async Task<User?> GetUserDataAsync(int userId)
    {
        User? user = userRepository.GetWithId(userId);
        if (user != null)
        {
            user.Password = null;
            return user;
        }
        else return null;
    }

    public async Task<User?> UpdateUserDataAsync(User user)
    {
        userRepository.Update(user);

        return await GetUserDataAsync(user.Id);
    }

    public async Task<bool> ChangeBalanceAsync(int userId, double sum)
    {
        if (sum == 0) return false;
        try
        {
            if (sum > 0) return await balanceService.AddToBalanceAsync(userId, sum);
            else return await balanceService.RemoveFromBalanceAsync(userId, sum);
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<User>> GetAllUsersAsync(int adminId)
    {
        User? admin = userRepository.GetWithId(adminId);
        if (admin == null || admin.Status != 3) throw new YouDoNotHavePermissionException();

        return userRepository.GetUserList();
    }
}

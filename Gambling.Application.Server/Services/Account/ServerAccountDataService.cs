using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;

namespace Gambling.Application.Server.Services.Account;

public class ServerAccountDataService(IBalanceService balanceService, IUserRepository userRepository) : IAccountDataService
{
    public async Task<UserModel?> GetUserDataAsync(int userId)
    {
        UserModel? user = await userRepository.GetWithIdAsync(userId);
        return user;
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

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        List<UserModel> users = await userRepository.GetUserListAsync();
        return users;
    }

    public async Task<bool> ChangeNameAsync(int userId, string name)
    {
        try
        {
            await userRepository.ChangeNameAsync(userId, name);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ChangeStatusAsync(int userId, int statusId)
    {
        try
        {
            await userRepository.ChangeStatusAsync(userId, statusId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> BlockUser(int userId)
    {
        try
        {
            await userRepository.ChangeIsBlockedAsync(userId, true);
            await ChangeStatusAsync(userId, 4);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UnblockUser(int userId)
    {
        try
        {
            await userRepository.ChangeIsBlockedAsync(userId, false);
            await ChangeStatusAsync(userId, 1);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
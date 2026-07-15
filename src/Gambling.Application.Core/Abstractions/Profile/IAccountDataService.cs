using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Profile;

public interface IAccountDataService
{
    public Task<UserModel?> GetUserDataAsync(int userId);
    public Task<List<UserModel>> GetAllUsersAsync();

    public Task<bool> ChangeNameAsync(int userId, string name);
    public Task<bool> ChangeBalanceAsync(int userId, double sum);
    public Task<bool> ChangeStatusAsync(int userId, int statusId);

    public Task<bool> BlockUser(int userId);
    public Task<bool> UnblockUser(int userId);
}
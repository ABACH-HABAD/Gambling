using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IUserRepository : IRepository
{
    public Task<UserModel?> GetWithIdAsync(int id);
    public Task<UserModel?> FindByEmailAsync(string email);

    public Task<bool> ComparePasswordAsync(int userId, string passwordHash);

    public Task<UserModel?> RegistrateAsync(string email, string hashedPassword);
    public Task<UserModel?> LoginAsyncAsync(string username, string hashedPassword);

    public Task<List<UserModel>> GetUserListAsync();

    public Task ChangeEmailAsync(int userId, string newEmail);
    public Task ChangePasswordAsync(int userId, string hashedPassword);
    public Task ChangeNameAsync(int userId, string newName);
    public Task ChangeStatusAsync(int userId, int statusId);
    public Task ChangeIsBlockedAsync(int userId, bool isBlocked);

    public Task WriteOffFromBalanceAsync(int userId, double value);
    public Task AddToBalanceAsync(int userId, double value);
    public Task UpdateGameStatsAsync(int userId, bool isGameWin, double balanceChange);
    public Task ChangeSlotsBonusGamesCountAsync(int userId, int slotBonusGamesChange);
}
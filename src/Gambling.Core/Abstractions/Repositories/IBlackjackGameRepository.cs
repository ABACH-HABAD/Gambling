using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IBlackjackGameRepository
{
    public Task<BlackjackGameModel> SaveGameStateAsync(int userId, BlackjackGameModel gameState);
    public Task DeleteGameStateAsync(int userId);
    public Task<bool> HasSaveGameAsync(int userId);
    public Task<BlackjackGameModel> GetSaveGameAsync(int userId);
}
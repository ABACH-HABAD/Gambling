using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IGameRepository : IRepository
{
    public Task AddGameAsync(int playerId, int gameType, double bet, bool isWin, double winAmount);

    public Task<List<GameModel>> GetGamesAsync();
    public Task<List<GameModel>> GetGamesAsync(int gameType);
    public Task<List<GameModel>> GetGamesAsync(int playerId, int gameType);
    public Task<List<GameModel>> GetGamesAsync(int playerId, int gameType, bool isWin);
}
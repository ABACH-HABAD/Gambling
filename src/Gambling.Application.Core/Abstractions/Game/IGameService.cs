using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Game;

public interface IGameService
{
    public Task<List<GameModel>> GetAllGamesAsync();
    public Task<List<GameModel>> GetAllGamesAsync(GameType type);
}
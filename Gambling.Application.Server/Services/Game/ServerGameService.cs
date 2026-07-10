using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;

namespace Gambling.Application.Server.Services.Game;

public class ServerGameService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    GameType defaultGameType = GameType.Any) : IGameService
{
    protected readonly IUserRepository _userRepository = userRepository;
    protected readonly IGameRepository _gameRepository = gameRepository;
    protected GameType DefaultGameType { get; init; } = defaultGameType;

    public async Task<List<GameModel>> GetAllGamesAsync(GameType type)
    {
        List<GameModel> gameEntities = DefaultGameType switch
        {
            GameType.None => [],
            GameType.Any => await _gameRepository.GetGamesAsync(),
            _ => await _gameRepository.GetGamesAsync((int)type),
        };

        return gameEntities;
    }

    public async Task<List<GameModel>> GetAllGamesAsync() => await GetAllGamesAsync(DefaultGameType);
}
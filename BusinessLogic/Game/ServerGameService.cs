using BusinessLogic.Exceptions;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Game;

public class ServerGameService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    GameType defaultGameType = GameType.Any) : IGameService
{
    protected readonly IUserRepository _userRepository = userRepository;
    protected readonly IGameRepository _gameRepository = gameRepository;
    protected GameType DefaultGameType { get; init; } = defaultGameType;

    public async Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId, GameType type)
    {
        DataBaseClasses.Entity.User? admin = _userRepository.GetWithId(adminId);
        if (admin == null || admin.Status != 3) throw new YouDoNotHavePermissionException();

        return DefaultGameType switch
        {
            GameType.None => [],
            GameType.Any => _gameRepository.GetGames(),
            _ => _gameRepository.GetGames((int)type),
        };
    }

    public async Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId) => await GetAllGamesAsync(adminId, DefaultGameType);

}

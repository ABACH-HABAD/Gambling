using DataBaseClasses.Entity;

namespace BusinessLogic.Game;

public interface IGameService
{
    public Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId);
    public Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId, GameType type);
}

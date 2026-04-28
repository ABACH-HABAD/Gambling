using DataBaseClasses.Entity;

namespace DataBaseClasses.Repository.Interfaces;

public interface IGameRepository : IRepository
{
    public void AddGame(int playerId, int gameType, double bid, bool isWin, double winAmount);
    public void AddGame(Game game);
}

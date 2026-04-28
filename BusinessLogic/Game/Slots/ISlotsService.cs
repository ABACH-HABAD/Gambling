using DataBaseClasses.Entity;

namespace BusinessLogic.Game.Slots;

public interface ISlotsService
{
    public Task<bool> HasBonusGames(int userId);
    public Task<SlotGameResult?> Spin(int userId, double bid, int linesCount = 3, int columnsCount = 5);
}

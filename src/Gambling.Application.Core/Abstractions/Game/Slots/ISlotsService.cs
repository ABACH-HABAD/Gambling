using Gambling.Application.Core.Api.Results;

namespace Gambling.Application.Core.Abstractions.Game.Slots;

public interface ISlotsService
{
    public Task<bool> HasBonusGamesAsync(int userId);
    public Task<SlotGameResult?> SpinAsync(int userId, double bid, int linesCount = 3, int columnsCount = 5);
}
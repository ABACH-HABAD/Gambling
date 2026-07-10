using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Application.Core.Abstractions.Game.Slots;

public interface ISlotsWinCounterService
{
    public double WinCount(SlotsCombo combo);
    public double TotalWinCount(List<SlotsCombo> combos);
    public int BonusCount(List<SlotsCombo> combos);
}

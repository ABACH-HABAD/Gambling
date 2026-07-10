using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Application.Core.Services.Game.Slots;

public class SlotsWinCounterService : ISlotsWinCounterService
{
    public double WinCount(SlotsCombo combo)
    {
        if (combo.Combo >= 3)
        {
            if (combo.Symbol == SlotElement.BONUS) return 0;

            foreach (var (name, _, win) in SlotElement.Variants)
            {
                if (name == combo.Symbol)
                {
                    return win * combo.Combo;
                }
            }
        }

        return 0;
    }

    public double TotalWinCount(List<SlotsCombo> combos)
    {
        double win = 0;
        foreach (SlotsCombo combo in combos) win += WinCount(combo);
        return win;
    }

    public int BonusCount(List<SlotsCombo> combos)
    {
        int bonus = 0;
        foreach (SlotsCombo combo in combos) bonus += combo.BonusCount;
        return bonus;
    }
}

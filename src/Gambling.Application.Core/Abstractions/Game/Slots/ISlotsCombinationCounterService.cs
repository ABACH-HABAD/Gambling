using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Application.Core.Abstractions.Game.Slots;

public interface ISlotsCombinationCounterService
{
    public int BonusCount(List<SlotElement> slots);
    public int BonusCount(List<List<SlotElement>> lines);
    public int ComboCountBySymbol(List<SlotElement> slots, string symbol);
    public List<int> ComboNumbersBySymbol(List<SlotElement> slots, string symbol);

    public SlotsCombo ComboCount(List<SlotElement> slots);

    public List<SlotsCombo> LinesComboCount(List<List<SlotElement>> lines);
    public List<SlotsCombo> ColumnsComboCount(List<List<SlotElement>> lines);
}

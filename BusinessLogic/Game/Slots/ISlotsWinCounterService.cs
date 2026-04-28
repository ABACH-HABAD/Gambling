namespace BusinessLogic.Game.Slots;

public interface ISlotsWinCounterService
{
    public double WinCount(SlotsCombo combo);
    public double TotalWinCount(List<SlotsCombo> combos);
    public int BonusCount(List<SlotsCombo> combos);
}

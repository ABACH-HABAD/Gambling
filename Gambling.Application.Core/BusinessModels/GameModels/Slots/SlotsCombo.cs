namespace Gambling.Application.Core.BusinessModels.GameModels.Slots;

public record class SlotsCombo(int Combo, string? Symbol, int BonusCount, List<int> ComboNumbers);

using BusinessLogic.Game.Slots;

namespace TestProject.Games.Slots;

public static class DisplayCombo
{
    public static string Display(this SlotsCombo combo)
    {
        return $"Комбо не собрано: кол-во: {combo.Combo}, символ: {combo.Symbol}, позиции: {combo.ComboNumbers.ToArray()}, бонусы: {combo.BonusCount}";
    }
}

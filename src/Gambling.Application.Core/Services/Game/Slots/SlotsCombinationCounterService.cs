using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Application.Core.Services.Game.Slots;

public class SlotsCombinationCounterService : ISlotsCombinationCounterService
{
    public int BonusCount(List<SlotElement> slots)
    {
        int bonusCount = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsBonus)
            {
                bonusCount++;
                continue;
            }
        }
        return bonusCount;
    }

    public int BonusCount(List<List<SlotElement>> lines)
    {
        int bonusCount = 0;
        foreach (List<SlotElement> line in lines)
        {
            bonusCount += BonusCount(line);
        }
        return bonusCount;
    }

    public int ComboCountBySymbol(List<SlotElement> slots, string symbol)
    {
        int combo = 0;
        int maxCombo = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsWild || slots[i].Text == symbol) combo++;
            else combo = 0;

            if (combo > maxCombo) maxCombo = combo;
        }

        return maxCombo;
    }

    public List<int> ComboNumbersBySymbol(List<SlotElement> slots, string symbol)
    {
        List<int> numbers = [];

        int combo = 0;
        int maxCombo = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsWild || slots[i].Text == symbol)
            {
                combo++;
                numbers.Add(i); 
            }
            else
            {
                combo = 0;
                numbers.Clear();
            }

            if (combo > maxCombo) maxCombo = combo;
        }

        if (maxCombo < 3) numbers.Clear();

        return numbers;
    }

    public SlotsCombo ComboCount(List<SlotElement> slots)
    {
        int combo;
        int maxCombo = 0;
        SlotElement? comboElement;
        SlotElement? maxComboElement = null;

        for (int i = 0; i < slots.Count; i++)
        {
            if (maxComboElement != null && maxComboElement.Text == slots[i].Text) continue;

            comboElement = slots[i];


            combo = ComboCountBySymbol(slots, comboElement.Text);

            if ((maxComboElement == null && !comboElement.IsWild) || 
                (maxComboElement == null && comboElement.IsWild && combo == slots.Count) ||
                (combo > maxCombo && maxCombo < 3 && !comboElement.IsWild ) || 
                    (maxComboElement != null &&
                    maxCombo >= 3 && 
                    combo >= 3 && 
                    (!comboElement.IsWild || (comboElement.IsWild && combo == slots.Count)) &&
                    ((combo * comboElement.Win) > (maxCombo * maxComboElement.Win))))
            {
                maxComboElement = comboElement;
                maxCombo = combo;
            }
        }

        int bounsCount = BonusCount(slots);
        if (maxComboElement != null && maxComboElement.IsBonus && maxCombo >= 3) bounsCount += (int)Math.Round(Math.Pow(maxCombo, 2));

        List<int> comboNumbers = maxComboElement == null ? [] : ComboNumbersBySymbol(slots, maxComboElement.Text);

        return new SlotsCombo(maxCombo, maxComboElement != null ? maxComboElement.Text : string.Empty, bounsCount, comboNumbers);
    }

    public List<SlotsCombo> LinesComboCount(List<List<SlotElement>> lines)
    {
        List<SlotsCombo> combos = [];
        for (int line = 0; line < lines.Count; line++)
        {
            combos.Add(ComboCount(lines[line]));
        }
        return combos;
    }

    public List<SlotsCombo> ColumnsComboCount(List<List<SlotElement>> lines)
    {
        if (lines.Count == 0) throw new ArgumentException("Должна быть хотя бы одна линия для подсчёта комбинаций");

        List<SlotsCombo> combos = [];

        for (int column = 0; column < lines[0].Count; column++)
        {
            List<SlotElement> columnElement = [];

            for (int line = 0; line < lines.Count; line++)
            {
                columnElement.Add(lines[line][column]);
            }

            combos.Add(ComboCount(columnElement));
        }

        return combos;
    }
}

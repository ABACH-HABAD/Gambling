namespace BusinessLogic.Game.Slots;

public class SlotElement
{
    public const string WILD = "Wild";
    public const string BONUS = "Bonus";

    public static List<(string name, int chance, double win)> Variants { get; } =
    [
        (WILD, 1, 100),
        (BONUS, 1, 50),

        ("Вишня", 4, 7.5),
        ("Звезда", 4, 10),
        ("BAR", 4, 10),
        ("seven", 4, 15),

        ("Арбуз", 10, 5),
        ("Апельсин", 10, 5),
        ("Лемон", 10, 5),
        ("Ананас", 10, 5),
        ("Яблоко", 10, 5),
        ("Виноград", 10, 5),
    ];

    private static readonly Random random = new();

    public string Text { get; init; }
    public double Win { get; init; }
    public bool IsBonus { get; init; }
    public bool IsWild { get; init; }

    public SlotElement()
    {
        List<(string text, double win)> baraban = [];
        foreach (var (name, chance, win) in Variants)
        {
            for (int i = 0; i < chance; i++)
            {
                baraban.Add((name, win));
            }
        }

        (Text, Win) = baraban[random.Next(baraban.Count)];

        IsWild = Text == WILD;
        IsBonus = Text == BONUS;
    }

    public override string ToString()
    {
        return Text;
    }

    public SlotElement(string requiredName)
    {
        foreach (var (name, _, win) in Variants)
        {
            if (name == requiredName)
            {
                Text = name;
                Win = win;
                IsWild = Text == WILD;
                IsBonus = Text == BONUS;

                return;
            }
        }

        throw new Exception($"Такой слот не существует: {requiredName}");
    }
}

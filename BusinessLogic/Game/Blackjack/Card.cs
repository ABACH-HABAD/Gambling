namespace BusinessLogic.Game.Blackjack;

public class Card
{
    public static string[] DenominationVariants { get; } =
    [
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "J",
        "Q",
        "K",
        "A",
    ];

    public static char[] SuitVariants { get; } = //♤♡♧♢
    [
        '♤',
        '♡',
        '♧',
        '♢',
    ];

    public string Denomination { get; init; }
    public char Suit { get; init; }

    private static readonly Random random = new();

    public int BlackjackScores { get; init; } //Стоимость карт: 2–10 = по номиналу. Валет, Дама, Король = 10 очков. Туз = 1 или 11 (в пользу игрока).

    public Card() 
    {
        Denomination = DenominationVariants[random.Next(0, DenominationVariants.Length)];
        Suit = SuitVariants[random.Next(0, SuitVariants.Length)];
    }

    public Card(string denomination, char suit)
    {
        if (DenominationVariants.Contains(denomination)) Denomination = denomination;
        else throw new Exception("Номинал не существует");

        if (SuitVariants.Contains(suit)) Suit = suit;
        else throw new Exception("Масть не существует");
    }
}

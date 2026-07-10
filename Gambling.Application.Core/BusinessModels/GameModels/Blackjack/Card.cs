using System.Drawing;

namespace Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

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

    public static Card UnkownCard() => new() { Denomination = "?", Suit = '?', IsOpen = false };

    private static readonly Random random = new();

    public string Denomination { get; init; }
    public char Suit { get; init; }
    public bool IsOpen { get; protected set; }

    public int BlackjackScores { get; init; } //Стоимость карт: 2–10 = по номиналу. Валет, Дама, Король = 10 очков. Туз = 1 или 11 (в пользу игрока).

    public string DisplayCard => $"{Denomination}{Suit}";
    public bool IsRed => Suit == '♡' || Suit == '♢';
    public bool IsBlack => Suit == '♧' || Suit == '♤';
    public Color DisplayCardColor => IsRed ? Color.Red : Color.Black; //Color.FromArgb(255, 255, 0, 0) : Color.FromArgb(255, 0, 0, 0);

    public Card()
    {
        Denomination = DenominationVariants[random.Next(0, DenominationVariants.Length)];
        Suit = SuitVariants[random.Next(0, SuitVariants.Length)];

        IsOpen = true;
    }

    public Card(bool isOpen)
    {
        Denomination = DenominationVariants[random.Next(0, DenominationVariants.Length)];
        Suit = SuitVariants[random.Next(0, SuitVariants.Length)];

        IsOpen = isOpen;
    }

    public Card(string denomination, char suit)
    {
        if (DenominationVariants.Contains(denomination)) Denomination = denomination;
        else throw new Exception("Номинал не существует");

        if (SuitVariants.Contains(suit)) Suit = suit;
        else throw new Exception("Масть не существует");

        IsOpen = true;
    }

    public Card(string denomination, char suit, bool isOpen)
    {
        if (DenominationVariants.Contains(denomination)) Denomination = denomination;
        else throw new Exception("Номинал не существует");

        if (SuitVariants.Contains(suit)) Suit = suit;
        else throw new Exception("Масть не существует");

        IsOpen = isOpen;
    }

    public void Open() => IsOpen = true;
    public void Close() => IsOpen = false;
}

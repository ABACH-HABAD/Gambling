namespace BusinessLogic.Game.Roulette;

public record class RouletteElement(int Value)
{
    private readonly static int[] blacks =
    [
        2,4,6,8,
        10,11,13,15,17,
        20,22,24,26,
        28,29,31,33,35
    ];

    private readonly static int[] reds =
    [
        1,3,5,7,9,
        12,14,16,18,
        19,21,23,25,27,
        30,32,34,36
    ];

    private readonly static int[] firstColumn =
    [
        1,4,7,10,
        13,16,19,22,
        25,28,31,34
    ];

    private readonly static int[] secondColumn =
    [
        2,5,8,11,
        14,17,20,23,
        26,29,32,35
    ];

    private readonly static int[] thirdColumn =
    [
        3,6,9,12,
        15,18,21,24,
        27,30,33,36
    ];

    public bool IsOdd => (Value % 2) != 0 && Value != 0;
    public bool IsEven => (Value % 2) == 0 && Value != 0;

    public bool IsFirstHalf => Value <= 18 && Value != 0;
    public bool IsSecondHalf => Value > 18 && Value != 0;

    public bool IsGreen => Value == 0;
    public bool IsRed => reds.Contains(Value);
    public bool IsBlack => blacks.Contains(Value);

    public bool IsFirst12 => Value >= 1 && Value <= 12 && Value != 0;
    public bool IsSecond12 => Value >= 13 && Value <= 24 && Value != 0;
    public bool IsThird12 => Value >= 25 && Value <= 36 && Value != 0;

    public bool IsFirstColumn => firstColumn.Contains(Value);
    public bool IsSecondColumn => secondColumn.Contains(Value);
    public bool IsThirdColumn => thirdColumn.Contains(Value);
}

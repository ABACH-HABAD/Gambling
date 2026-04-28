using Google.Protobuf.WellKnownTypes;
using System.Security.Cryptography;

namespace BusinessLogic.Game.Roulette;

public class RouletteBid
{
    public static Dictionary<string, double> BidTypes { get; } = new Dictionary<string, double>()
    {
        {"0", 36 },
        {"1", 36 },
        {"2", 36 },
        {"3", 36 },
        {"4", 36 },
        {"5", 36 },
        {"6", 36 },
        {"7", 36 },
        {"8", 36 },
        {"9", 36 },
        {"10", 36 },
        {"11", 36 },
        {"12", 36 },
        {"13", 36 },
        {"14", 36 },
        {"15", 36 },
        {"16", 36 },
        {"17", 36 },
        {"18", 36 },
        {"19", 36 },
        {"20", 36 },
        {"21", 36 },
        {"22", 36 },
        {"23", 36 },
        {"24", 36 },
        {"25", 36 },
        {"26", 36 },
        {"27", 36 },
        {"28", 36 },
        {"29", 36 },
        {"30", 36 },
        {"31", 36 },
        {"32", 36 },
        {"33", 36 },
        {"34", 36 },
        {"35", 36 },
        {"36", 36 },
        {"Red", 2},
        {"Black", 2 },
        {"Odd", 2 },
        {"Even", 2 },
        {"FirstHalf", 2 },
        {"SecondHalf", 2 },
        {"First12", 3 },
        {"Second12", 3 },
        {"Third12", 3 },
        {"FirstColumn", 3 },
        {"SecondColumn", 3 },
        {"ThirdColumn", 3 },
    };

    private string type = null!;

    public string Type 
    { 
        get => type; 
        set
        {
            if (BidTypes.ContainsKey(value)) type = value;
            else throw new ArgumentException("Такого типа ставки не существует");
        }
    }
    public double Coefficent => BidTypes[type];
    public double BidCount { get; set; }

    public RouletteBid(RouletteBidJson rouletteBidJson)
    {
        Type = rouletteBidJson.Type ?? string.Empty;
        BidCount = rouletteBidJson.BidCount;
    }

    public RouletteBid(string type)
    {
        Type = type;
    }

    public RouletteBid(int bidCount, string type)
    {
        Type = type;
        BidCount = bidCount;
    }

    public RouletteBidJson PrepareToJson() => new() { Type = Type, BidCount = BidCount };
}

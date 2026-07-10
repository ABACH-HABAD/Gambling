using Gambling.Application.Core.BusinessModels.GameModels.Roulette;

namespace Gambling.Application.Core.Services.Game.Roulette;

public static class BidPrepareToJson
{
    public static List<RouletteBidJson> PrepareToJson(this List<RouletteBid> bids)
    {
        List<RouletteBidJson> list = [];
        foreach (RouletteBid bid in bids)
        {
            list.Add(bid.PrepareToJson());
        }
        return list;
    }

    public static List<RouletteBid> PrepareToJson(this List<RouletteBidJson> bids)
    {
        List<RouletteBid> list = [];
        foreach (RouletteBidJson bid in bids)
        {
            list.Add(new RouletteBid(bid));
        }
        return list;
    }
}